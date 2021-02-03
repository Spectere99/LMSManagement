using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class InsuranceCompany
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool Archived { get; set; }
    }
}
