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
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public int RecordId { get; set; }
        public Boolean Priority { get; set; }
        public Boolean Completed { get; set; }
        public DateTime? CompletedTimeStamp { get; set; }
        public int FileUploadLogId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DrugName { get; set; }
        public int InsuranceCompany_Id { get; set; }
        public int Status { get; set; }
        public int BillingStatus { get; set; }
        public DateTime Submitted { get; set; }
        public DateTime? Approval { get; set; }
        public DateTime? Denial { get; set; }
        public string ApprovalDocumentUrl { get; set; }
        public string Note { get; set; }
        public DateTime? Assigned { get; set; }
        public string AssignedTo { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool Archived { get; set; }
    }
}
