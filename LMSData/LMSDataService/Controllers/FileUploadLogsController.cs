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
    public class FileUploadLogsController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/FileUploadLogs
        public IQueryable<FileUploadLog> GetFileUploadLogs()
        {
            return db.FileUploadLogs;
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
            catch (DbUpdateConcurrencyException)
            {
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FileUploadLogs.Add(fileUploadLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fileUploadLog.Id }, fileUploadLog);
        }

        // DELETE: api/FileUploadLogs/5
        [ResponseType(typeof(FileUploadLog))]
        public IHttpActionResult DeleteFileUploadLog(int id)
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