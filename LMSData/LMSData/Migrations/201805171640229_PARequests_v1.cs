namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileUploadLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uploaded = c.DateTime(nullable: false),
                        FileName = c.String(),
                        SourceIpAddress = c.String(),
                        RecordCount = c.Int(nullable: false),
                        SuccessCount = c.Int(nullable: false),
                        FailureCount = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Module_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.Module_Id)
                .Index(t => t.Module_Id);
            
            CreateTable(
                "dbo.InsuranceCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaRequestAudits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RecordId = c.Int(nullable: false),
                        PatientName = c.String(),
                        DoctorName = c.String(),
                        DrugName = c.String(),
                        Submitted = c.DateTime(nullable: false),
                        Approval = c.DateTime(nullable: false),
                        Denial = c.DateTime(nullable: false),
                        ApprovalDocumentUrl = c.String(),
                        Note = c.String(),
                        AssignedTo = c.String(),
                        Archived = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        InsuranceCompany_Id = c.Int(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InsuranceCompanies", t => t.InsuranceCompany_Id)
                .ForeignKey("dbo.Lookups", t => t.Status_Id)
                .Index(t => t.InsuranceCompany_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.PaRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientName = c.String(),
                        DoctorName = c.String(),
                        DrugName = c.String(),
                        Submitted = c.DateTime(nullable: false),
                        Approval = c.DateTime(nullable: false),
                        Denial = c.DateTime(nullable: false),
                        ApprovalDocumentUrl = c.String(),
                        Note = c.String(),
                        Assigned = c.DateTime(nullable: false),
                        AssignedTo = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                        InsuranceCompany_Id = c.Int(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InsuranceCompanies", t => t.InsuranceCompany_Id)
                .ForeignKey("dbo.Lookups", t => t.Status_Id)
                .Index(t => t.InsuranceCompany_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasswordHash = c.String(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTime(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaRequests", "Status_Id", "dbo.Lookups");
            DropForeignKey("dbo.PaRequests", "InsuranceCompany_Id", "dbo.InsuranceCompanies");
            DropForeignKey("dbo.PaRequestAudits", "Status_Id", "dbo.Lookups");
            DropForeignKey("dbo.PaRequestAudits", "InsuranceCompany_Id", "dbo.InsuranceCompanies");
            DropForeignKey("dbo.FileUploadLogs", "Module_Id", "dbo.Lookups");
            DropIndex("dbo.PaRequests", new[] { "Status_Id" });
            DropIndex("dbo.PaRequests", new[] { "InsuranceCompany_Id" });
            DropIndex("dbo.PaRequestAudits", new[] { "Status_Id" });
            DropIndex("dbo.PaRequestAudits", new[] { "InsuranceCompany_Id" });
            DropIndex("dbo.FileUploadLogs", new[] { "Module_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserLogins");
            DropTable("dbo.PaRequests");
            DropTable("dbo.PaRequestAudits");
            DropTable("dbo.InsuranceCompanies");
            DropTable("dbo.FileUploadLogs");
        }
    }
}
