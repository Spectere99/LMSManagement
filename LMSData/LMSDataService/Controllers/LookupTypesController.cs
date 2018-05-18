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
using log4net;
using LIMSData;
using LIMSData.DBObjects;

namespace LMSDataService.Controllers
{
    public class LookupTypesController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );

        // GET: api/LookupTypes
        public IHttpActionResult GetLookupTypes(HttpRequestMessage request)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            bool showArchived = false;

            if (headers.Contains("showArchived"))
            {
                showArchived = Boolean.Parse(headers.GetValues("showArchived").First());
            }

            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling GET request from user: {0}", user);

                try
                {
                    if (showArchived)
                    { 
                        return Ok(db.LookupTypes);
                    }

                    return Ok(db.LookupTypes.Where(p => p.Archived == false).ToList());
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while getting Lookup Types.", e);
                    return InternalServerError(e);
                }
            }
            return BadRequest("Header value <userid> not found.");
        }

        // GET: api/LookupTypes/5
        [ResponseType(typeof(LookupType))]
        public IHttpActionResult GetLookupType(int id)
        {
            LookupType lookupType = db.LookupTypes.Find(id);
            if (lookupType == null)
            {
                return NotFound();
            }

            return Ok(lookupType);
        }

        // PUT: api/LookupTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, LookupType lookupType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lookupType.Id)
            {
                return BadRequest();
            }

            db.Entry(lookupType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupTypeExists(id))
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

        // POST: api/LookupTypes
        [ResponseType(typeof(LookupType))]
        public IHttpActionResult PostLookupType(LookupType lookupType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LookupTypes.Add(lookupType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lookupType.Id }, lookupType);
        }

        // DELETE: api/LookupTypes/5
        [ResponseType(typeof(LookupType))]
        public IHttpActionResult DeleteLookupType(int id)
        {
            LookupType lookupType = db.LookupTypes.Find(id);
            if (lookupType == null)
            {
                return NotFound();
            }

            db.LookupTypes.Remove(lookupType);
            db.SaveChanges();

            return Ok(lookupType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LookupTypeExists(int id)
        {
            return db.LookupTypes.Count(e => e.Id == id) > 0;
        }
    }
}