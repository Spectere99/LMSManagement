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
    public class InsuranceCompaniesController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();
        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );

        // GET: api/InsuranceCompanies
        public IQueryable<InsuranceCompany> GetInsuranceCompanies()
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                return db.InsuranceCompanies;
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "GetInsuranceCompanies"), e);
                throw;
            }
            
        }

        // GET: api/InsuranceCompanies/5
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult GetInsuranceCompany(int id)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
                if (insuranceCompany == null)
                {
                    return NotFound();
                }

                return Ok(insuranceCompany);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "GetInsuranceCompany"), e);
                throw;
            }
        }

        // PUT: api/InsuranceCompanies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInsuranceCompany(int id, InsuranceCompany insuranceCompany)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insuranceCompany.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(insuranceCompany).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyExists(id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "PutInsuranceCompany"), e);
                throw;
            }
        }

        // POST: api/InsuranceCompanies
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult PostInsuranceCompany(InsuranceCompany insuranceCompany)
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
                db.InsuranceCompanies.Add(insuranceCompany);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = insuranceCompany.Id }, insuranceCompany);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "PostInsuranceCompany"), e);
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/InsuranceCompanies/5
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult DeleteInsuranceCompany(int id)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(Resource.LogDebugModeMessage);
            }

            try
            {
                InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
                if (insuranceCompany == null)
                {
                    return NotFound();
                }

                db.InsuranceCompanies.Remove(insuranceCompany);
                db.SaveChanges();

                return Ok(insuranceCompany);
            }
            catch (Exception e)
            {
                _log.Error(string.Format(Resource.GeneralError_Pre, "DeleteInsuranceCompany"), e);
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

        private bool InsuranceCompanyExists(int id)
        {
            return db.InsuranceCompanies.Count(e => e.Id == id) > 0;
        }
    }
}