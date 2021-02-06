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
    public class LookupsController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();
        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );
        // GET: api/Lookups
        public IHttpActionResult GetLookups(HttpRequestMessage request)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
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

                if (headers.Contains("lookupTypeId"))
                {
                    var lookupTypeId = 0;
                    var result = int.TryParse(headers.GetValues("lookupTypeId").First(), out lookupTypeId);
                    return Ok(db.Lookups.Where(p => p.LookupTypeId == lookupTypeId).ToList());
                }

                try
                {
                    if (showArchived)
                    {
                        return Ok(db.Lookups);
                    }

                    return Ok(db.Lookups.Where(p => p.Archived == false).ToList());
                }
                catch (Exception e)
                {
                    _log.Error(string.Format(Resource.GeneralError_Pre, "GetLookups"), e);
                    return InternalServerError(e);
                }
            }
            return BadRequest("Header value <userid> not found.");
        }

        // GET: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult GetLookup(int id)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                Lookup lookup = db.Lookups.Find(id);
                if (lookup == null)
                {
                    return NotFound();
                }

                return Ok(lookup);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "GetLookup"), e);
                return InternalServerError(e);
            }
        }

        // PUT: api/Lookups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLookup(int id, Lookup lookup)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lookup.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(lookup).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LookupExists(id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "PutLookup"), e);
                return InternalServerError(e);
            }
        }

        // POST: api/Lookups
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult PostLookup(Lookup lookup)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Lookups.Add(lookup);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = lookup.Id }, lookup);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "PostLookup"), e);
                return InternalServerError(e);
            }
            
        }

        // DELETE: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult DeleteLookup(int id)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                Lookup lookup = db.Lookups.Find(id);
                if (lookup == null)
                {
                    return NotFound();
                }

                db.Lookups.Remove(lookup);
                db.SaveChanges();

                return Ok(lookup);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "DeleteLookup"), e);
                return InternalServerError(e);
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LookupExists(int id)
        {
            return db.Lookups.Count(e => e.Id == id) > 0;
        }
    }
}