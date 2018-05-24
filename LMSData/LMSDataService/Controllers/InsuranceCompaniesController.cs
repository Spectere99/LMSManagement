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
    public class InsuranceCompaniesController : ApiController
    {
        private LMSDataDBContext db = new LMSDataDBContext();

        // GET: api/InsuranceCompanies
        public IQueryable<InsuranceCompany> GetInsuranceCompanies()
        {
            return db.InsuranceCompanies;
        }

        // GET: api/InsuranceCompanies/5
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult GetInsuranceCompany(int id)
        {
            InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }

            return Ok(insuranceCompany);
        }

        // PUT: api/InsuranceCompanies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInsuranceCompany(int id, InsuranceCompany insuranceCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insuranceCompany.Id)
            {
                return BadRequest();
            }

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
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/InsuranceCompanies
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult PostInsuranceCompany(InsuranceCompany insuranceCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InsuranceCompanies.Add(insuranceCompany);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = insuranceCompany.Id }, insuranceCompany);
        }

        // DELETE: api/InsuranceCompanies/5
        [ResponseType(typeof(InsuranceCompany))]
        public IHttpActionResult DeleteInsuranceCompany(int id)
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