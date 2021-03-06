﻿using System;
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
    public class PaRequestsController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/PaRequests
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

                    if (headers.Contains("Id"))
                    {

                        var id = 0;
                        int.TryParse(headers.GetValues("Id").First(), out id);
                        IQueryable<PaRequest> filteredResults = db.PaRequests.Where(p => p.FileUploadLogId == id).Include(t => t.FileUploadLog);

                        if (headers.Contains("AssignedTo"))
                        {
                            int assignedToId = 0;
                            var res = int.TryParse(headers.GetValues("AssignedTo").First(), out assignedToId);
                            IQueryable<PaRequest> assignedToFilteredRequests =
                                filteredResults.Where(p => p.AssignedToId == assignedToId && p.Archived==false);

                            return Ok(assignedToFilteredRequests);
                        }
                        return Ok(filteredResults);
                    }

                    var showArchived = false;

                    if (headers.Contains("showArchived"))
                    {
                        var archInd = Boolean.Parse(headers.GetValues("showArchived").First());
                        showArchived = archInd;
                    }
                    if (headers.Contains("AssignedTo"))
                    {
                        int assignedToId = 0;
                        var res = int.TryParse(headers.GetValues("AssignedTo").First(), out assignedToId);
                        IQueryable<PaRequest> results = null;
                        if (showArchived)
                        {
                            results = db.PaRequests.Where(p => p.AssignedToId == assignedToId)
                                .Include(t => t.FileUploadLog)
                                .Include(t=>t.AssignedTo);
                            return Ok(results);
                        }

                        results = db.PaRequests.Where(p => p.AssignedToId == assignedToId && p.Archived==false)
                            .Include(t => t.FileUploadLog)
                            .Include(t=>t.AssignedTo);
                        // results = db.PaRequests.Where(p => p.Archived == false);
                        return Ok(results);


                    }

                    IQueryable<PaRequest> fullResults = null;
                    if (showArchived)
                    {
                        fullResults = db.PaRequests
                                        .Include(t => t.FileUploadLog)
                                        .Include(t=>t.AssignedTo);
                        return Ok(fullResults);
                    }
                    fullResults = db.PaRequests.Where(p=>p.Archived==false)
                        .Include(t => t.FileUploadLog)
                        .Include(t=>t.AssignedTo);
                    return Ok(fullResults);
                }

                throw new Exception(validationResult.Item2);
            }
            if (headers.Contains("userid"))
            {
                if (headers.Contains("Id"))
                {
                    var id = int.Parse(headers.GetValues("Id").First());
                    IQueryable<PaRequest> filteredResults = db.PaRequests.Where(p => p.FileUploadLogId == id)
                        .Include(t=>t.FileUploadLog)
                        .Include(t => t.AssignedTo); ;
                    return Ok(filteredResults);

                }

                IQueryable<PaRequest> results = db.PaRequests
                                                    .Include(t=>t.FileUploadLog)
                                                    .Include(t=>t.AssignedTo);
                return Ok(results);

            }

            return null;
        }

        // GET: api/PaRequests/5
        [ResponseType(typeof(PaRequest))]
        public IHttpActionResult GetPaRequest(int id)
        {
            PaRequest paRequest = db.PaRequests.Find(id);
            if (paRequest == null)
            {
                return NotFound();
            }

            return Ok(paRequest);
        }

        // PUT: api/PaRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaRequest(int id, PaRequest paRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paRequest.Id)
            {
                return BadRequest();
            }

            // paRequest.CompletedTimeStamp = DateTime.Now;
            db.Entry(paRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PaRequests
        [ResponseType(typeof(PaRequest))]
        public IHttpActionResult PostPaRequest(PaRequest paRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaRequests.Add(paRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paRequest.Id }, paRequest);
        }

        // DELETE: api/PaRequests/5
        [ResponseType(typeof(PaRequest))]
        public IHttpActionResult DeletePaRequest(int id)
        {
            PaRequest paRequest = db.PaRequests.Find(id);
            if (paRequest == null)
            {
                return NotFound();
            }

            db.PaRequests.Remove(paRequest);
            db.SaveChanges();

            return Ok(paRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaRequestExists(int id)
        {
            return db.PaRequests.Count(e => e.Id == id) > 0;
        }
    }
}