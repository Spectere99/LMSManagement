using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Description;
using LIMSData;
using LIMSData.DBObjects;

namespace LMSDataService.Controllers
{
    public class PaRequestNotesController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/PaRequestNotes
        public IHttpActionResult GetPaRequests(HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("token"))
            {
                var userToken = headers.GetValues("token").First();
                Tuple<bool, string> validationResult = JwtTokenHelper.ValidateToken(userToken);
                if (validationResult.Item1)
                {
                    // Check to see if the refreshId's match.  If they do not, then it means something (i.e. invalid password)
                    //   has caused the issued token to become invalidated.  If so, then we need to send a message back to 
                    //   the calling client.
                    long refreshId = 0;
                    if (long.TryParse(JwtTokenHelper.GetTokenPayloadValue(userToken, "refreshId"), out refreshId))
                    {
                        if (db.UserLogins.FirstOrDefault(p => p.RefreshId == refreshId) == null)
                        {
                            return Content(HttpStatusCode.Unauthorized,
                                "Token failed refresh check, User account disabled/locked-out");
                            // throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Token failed refresh check.  User account is disabled/locked-out.")});
                        }
                        
                    }

                     if (headers.Contains("RequestId"))
                    {
                        int id = 0;
                        var result = int.TryParse(headers.GetValues("RequestId").First(), out id);
                        IQueryable<PaRequestNote> filteredResults = db.PaRequestNotes.Where(p => p.PaRequestId == id).Include(t => t.PaRequest);

                        return Ok(filteredResults);
                    }

                    var showArchived = false;

                    if (headers.Contains("showArchived"))
                    {
                        var archInd = Boolean.Parse(headers.GetValues("showArchived").First());
                        showArchived = archInd;
                    }
                    

                    IQueryable<PaRequestNote> fullResults = null;
                    if (showArchived)
                    {
                        fullResults = db.PaRequestNotes.Include(t => t.PaRequest);
                        return Ok(fullResults);
                    }
                    fullResults = db.PaRequestNotes.Where(p=>p.Archived==false).Include(t => t.PaRequest);
                    return Ok(fullResults);
                }

                throw new Exception(validationResult.Item2);
            }
            if (headers.Contains("userid"))
            {
                if (headers.Contains("Id"))
                {
                    var id = int.Parse(headers.GetValues("Id").First());
                    IQueryable<PaRequestNote> filteredResults = db.PaRequestNotes.Where(p => p.PaRequestId == id).Include(t=>t.PaRequest);
                    return Ok(filteredResults);

                }

                IQueryable<PaRequestNote> results = db.PaRequestNotes.Include(t=>t.PaRequest);
                return Ok(results);

            }

            return null;
        }

        // GET: api/PaRequestNotes/5
        [ResponseType(typeof(PaRequestNote))]
        public IHttpActionResult GetPaRequestNotes(int id)
        {
            PaRequestNote paRequestNote = db.PaRequestNotes.Find(id);
            if (paRequestNote == null)
            {
                return NotFound();
            }

            return Ok(paRequestNote);
        }

        // PUT: api/PaRequestNotes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaRequestNote(int id, PaRequestNote paRequestNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paRequestNote.Id)
            {
                return BadRequest();
            }
            var headers = Request.Headers;

            if (headers.Contains("token"))
            {
                var userToken = headers.GetValues("token").First();

                string userName = JwtTokenHelper.GetTokenPayloadValue(userToken, "unique_name");
                string userRole = JwtTokenHelper.GetTokenPayloadValue(userToken, "role");

                if (userRole != "Administrator") //Need to check that the userName is the same as the created by
                {
                    if (userName != paRequestNote.CreatedBy)
                    {
                        return BadRequest("Editing user is not an Administrator or did not create the original note.");
                    }
                }
                // paRequest.CompletedTimeStamp = DateTime.Now;
                paRequestNote.LastModified = DateTime.Now;
                paRequestNote.LastModifiedBy = userName;

                db.Entry(paRequestNote).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaRequestNoteExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PaRequestNotes
        [ResponseType(typeof(PaRequest))]
        public IHttpActionResult PostPaRequestNotes(PaRequestNote paRequestNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var headers = Request.Headers;

            if (headers.Contains("token"))
            {
                var userToken = headers.GetValues("token").First();
                
                string userName = JwtTokenHelper.GetTokenPayloadValue(userToken, "unique_name");
                paRequestNote.Created = DateTime.Now;
                paRequestNote.CreatedBy = userName;
                paRequestNote.LastModified = DateTime.Now;
                paRequestNote.LastModifiedBy = userName;

                db.PaRequestNotes.Add(paRequestNote);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = paRequestNote.Id }, paRequestNote);
        }

        // DELETE: api/PaRequestNotes/5
        [ResponseType(typeof(PaRequestNote))]
        public IHttpActionResult DeletePaRequestNote(int id)
        {
            PaRequestNote paRequestNote = db.PaRequestNotes.Find(id);
            if (paRequestNote == null)
            {
                return NotFound();
            }

            db.PaRequestNotes.Remove(paRequestNote);
            db.SaveChanges();

            return Ok(paRequestNote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaRequestNoteExists(int id)
        {
            return db.PaRequestNotes.Count(e => e.Id == id) > 0;
        }
    }
}