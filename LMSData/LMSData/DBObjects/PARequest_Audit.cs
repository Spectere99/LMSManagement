using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class PaRequestAudit
    {
        public long Id { get; set; }
        public int RecordId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DrugName { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
        public Lookup Status { get; set; }
        public DateTime Submitted { get; set; }
        public DateTime Approval { get; set; }
        public DateTime Denial { get; set; }
        public string ApprovalDocumentUrl { get; set; }
        public string Note { get; set; }
        public string AssignedTo { get; set; }
        public bool Archived { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
