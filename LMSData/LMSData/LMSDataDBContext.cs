using LIMSData.DBObjects;
using System.Data.Entity;

namespace LIMSData
{
    // ReSharper disable once InconsistentNaming
    public class LMSDataDBContext : DbContext
    {
        public DbSet<Lookup> Lookups { get; set; }        
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<FileUploadLog> FileUploadLogs { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set;  }
        public DbSet<PaRequest> PaRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<PaRequestAudit> PaRequestAudits { get; set; }
        public DbSet<UserLoginAudit> UserLoginAudits { get; set; }

        public LMSDataDBContext() : base("name=LMSDataDBContext")
        {

        }
    }
}
