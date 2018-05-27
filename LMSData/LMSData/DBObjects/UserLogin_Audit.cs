using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class UserLoginAudit
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public int RecordId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public long RefreshId { get; set; }
    }
}

