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
    public class FileUploadLogsController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();
        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );
        // GET: api/FileUploadLogs
        public IQueryable<FileUploadLog> GetFileUploadLogs(HttpRequestMessage request)
        {
            var headers = request.Headers;
            DateTime logStartDate;
            DateTime logEndDate;

            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                if (headers.Contains("logStartDate"))
                {
                    try
                    {
                        var dateVal = headers.GetValues("logStartDate").First();
                        logStartDate = DateTime.Parse(headers.GetValues("logStartDate").First());
                        logStartDate = logStartDate.Date;
                        logEndDate = logStartDate.AddDays(1);
                        return db.FileUploadLogs.Where(p => p.Uploaded >= logStartDate && p.Uploaded <= logEndDate && p.Archived == false);
                    }
                    catch (Exception e)
                    {
                        _log.Error(string.Format(Resource.GeneralError_Pre, "GetFileUploadLogs"), e);
                        throw new Exception("Invalid Request", e);
                    }
                }

                // var showArchived = false;

                if (headers.Contains("showArchived"))
                {
                    var archInd = Boolean.Parse(headers.GetValues("showArchived").First());
                    if (archInd)
                    {
                        return db.FileUploadLogs;
                    }

                    return db.FileUploadLogs.Where(p => p.Archived == false);
                }

                return db.FileUploadLogs.Where(p => p.Archived == false);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "GetFileUploadLogs"), e);
                throw;
            }
            
        }

        // GET: api/FileUploadLogs/5
        [ResponseType(typeof(FileUploadLog))]
        public IHttpActionResult GetFileUploadLog(int id)
        {
            FileUploadLog fileUploadLog = db.FileUploadLogs.Find(id);
            if (fileUploadLog == null)
            {
                return NotFound();
            }

            return Ok(fileUploadLog);
        }

        // PUT: api/FileUploadLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFileUploadLog(int id, FileUploadLog fileUploadLog)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileUploadLog.Id)
            {
                return BadRequest();
            }

            db.Entry(fileUploadLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbException)
            {
                _log.Error(string.Format(Resource.DBConcurrencyError_Pre, "PutFileUploadLog"), dbException);
                if (!FileUploadLogExists(id))
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

        // POST: api/FileUploadLogs
        [ResponseType(typeof(FileUploadLog))]
        public IHttpActionResult PostFileUploadLog(FileUploadLog fileUploadLog)
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
                db.FileUploadLogs.Add(fileUploadLog);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = fileUploadLog.Id }, fileUploadLog);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "PostFileUploadLog"), e);
                throw;
            }
        }

        // DELETE: api/FileUploadLogs/5
        [ResponseType(typeof(FileUploadLog))]
        public IHttpActionResult DeleteFileUploadLog(int id)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                FileUploadLog fileUploadLog = db.FileUploadLogs.Find(id);
                if (fileUploadLog == null)
                {
                    return NotFound();
                }

                db.FileUploadLogs.Remove(fileUploadLog);
                db.SaveChanges();

                return Ok(fileUploadLog);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "DeleteFileUploadLog"), e);
                throw;
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

        private bool FileUploadLogExists(int id)
        {
            return db.FileUploadLogs.Count(e => e.Id == id) > 0;
        }
    }
}