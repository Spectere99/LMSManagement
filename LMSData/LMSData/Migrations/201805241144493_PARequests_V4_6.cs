namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaRequestAudits", "InsuranceCompany_Id", "dbo.InsuranceCompanies");
            DropForeignKey("dbo.PaRequestAudits", "Status_Id", "dbo.Lookups");
            DropIndex("dbo.PaRequestAudits", new[] { "InsuranceCompany_Id" });
            DropIndex("dbo.PaRequestAudits", new[] { "Status_Id" });
            AddColumn("dbo.PaRequestAudits", "Action", c => c.String());
            AddColumn("dbo.PaRequestAudits", "FileUploadLogId", c => c.Int(nullable: false));
            AddColumn("dbo.PaRequestAudits", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.PaRequestAudits", "Assigned", c => c.DateTime());
            AddColumn("dbo.PaRequestAudits", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaRequestAudits", "CreatedBy", c => c.String());
            AddColumn("dbo.PaRequestAudits", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaRequestAudits", "LastModifiedBy", c => c.String());
            AlterColumn("dbo.PaRequestAudits", "Approval", c => c.DateTime());
            AlterColumn("dbo.PaRequestAudits", "Denial", c => c.DateTime());
            AlterColumn("dbo.PaRequestAudits", "InsuranceCompany_Id", c => c.Int(nullable: false));
            DropColumn("dbo.PaRequestAudits", "ModifiedDate");
            DropColumn("dbo.PaRequestAudits", "ModifiedBy");
            DropColumn("dbo.PaRequestAudits", "Status_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaRequestAudits", "Status_Id", c => c.Int());
            AddColumn("dbo.PaRequestAudits", "ModifiedBy", c => c.String());
            AddColumn("dbo.PaRequestAudits", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaRequestAudits", "InsuranceCompany_Id", c => c.Int());
            AlterColumn("dbo.PaRequestAudits", "Denial", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaRequestAudits", "Approval", c => c.DateTime(nullable: false));
            DropColumn("dbo.PaRequestAudits", "LastModifiedBy");
            DropColumn("dbo.PaRequestAudits", "LastModified");
            DropColumn("dbo.PaRequestAudits", "CreatedBy");
            DropColumn("dbo.PaRequestAudits", "Created");
            DropColumn("dbo.PaRequestAudits", "Assigned");
            DropColumn("dbo.PaRequestAudits", "Status");
            DropColumn("dbo.PaRequestAudits", "FileUploadLogId");
            DropColumn("dbo.PaRequestAudits", "Action");
            CreateIndex("dbo.PaRequestAudits", "Status_Id");
            CreateIndex("dbo.PaRequestAudits", "InsuranceCompany_Id");
            AddForeignKey("dbo.PaRequestAudits", "Status_Id", "dbo.Lookups", "Id");
            AddForeignKey("dbo.PaRequestAudits", "InsuranceCompany_Id", "dbo.InsuranceCompanies", "Id");
        }
    }
}
