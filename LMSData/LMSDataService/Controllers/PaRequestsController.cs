using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IQueryable<PaRequest> GetPaRequests(HttpRequestMessage request)
        {
            var headers = request.Headers;
            if (headers.Contains("userid"))
            {
                if (headers.Contains("Id"))
                {
                    var id = int.Parse(headers.GetValues("Id").First());
                    return db.PaRequests.Where(p => p.FileUploadLogId == id).Include(t=>t.FileUploadLog);
                }

                return db.PaRequests.Include(t=>t.FileUploadLog);

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