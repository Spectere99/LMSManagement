namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileUploadLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchName = c.String(),
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
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupTypeId = c.Int(nullable: false),
                        LookupValue = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LookupTypes", t => t.LookupTypeId, cascadeDelete: true)
                .Index(t => t.LookupTypeId);
            
            CreateTable(
                "dbo.InsuranceCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyCode = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LookupTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaRequestAudits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        RecordId = c.Int(nullable: false),
                        Priority = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        CompletedTimeStamp = c.DateTime(),
                        FileUploadLogId = c.Int(nullable: false),
                        PatientName = c.String(),
                        DoctorName = c.String(),
                        DrugName = c.String(),
                        InsuranceCompany_Id = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        BillingStatus = c.Int(nullable: false),
                        Submitted = c.DateTime(nullable: false),
                        Approval = c.DateTime(),
                        Denial = c.DateTime(),
                        ApprovalDocumentUrl = c.String(),
                        Note = c.String(),
                        Assigned = c.DateTime(),
                        AssignedTo = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileUploadLogId = c.Int(nullable: false),
                        Priority = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        CompletedTimeStamp = c.DateTime(),
                        PatientName = c.String(),
                        DoctorName = c.String(),
                        DrugName = c.String(),
                        InsuranceCompany_Id = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        BillingStatus = c.Int(nullable: false),
                        Submitted = c.DateTime(nullable: false),
                        Approval = c.DateTime(),
                        Denial = c.DateTime(),
                        ApprovalDocumentUrl = c.String(),
                        Note = c.String(),
                        Assigned = c.DateTime(),
                        AssignedTo = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploadLogs", t => t.FileUploadLogId, cascadeDelete: true)
                .Index(t => t.FileUploadLogId);
            
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
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaRequests", "FileUploadLogId", "dbo.FileUploadLogs");
            DropForeignKey("dbo.Lookups", "LookupTypeId", "dbo.LookupTypes");
            DropForeignKey("dbo.FileUploadLogs", "Module_Id", "dbo.Lookups");
            DropIndex("dbo.PaRequests", new[] { "FileUploadLogId" });
            DropIndex("dbo.Lookups", new[] { "LookupTypeId" });
            DropIndex("dbo.FileUploadLogs", new[] { "Module_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserLogins");
            DropTable("dbo.PaRequests");
            DropTable("dbo.PaRequestAudits");
            DropTable("dbo.LookupTypes");
            DropTable("dbo.InsuranceCompanies");
            DropTable("dbo.Lookups");
            DropTable("dbo.FileUploadLogs");
        }
    }
}
