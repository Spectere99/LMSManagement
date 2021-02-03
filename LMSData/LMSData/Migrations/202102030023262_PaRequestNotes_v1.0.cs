namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequestNotes_v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaRequestNoteAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        RecordId = c.Int(nullable: false),
                        PaRequestId = c.Int(nullable: false),
                        NoteText = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaRequestNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaRequestId = c.Int(nullable: false),
                        NoteText = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.InsuranceCompanies", "PhoneNumber", c => c.String());
            AddColumn("dbo.InsuranceCompanies", "FaxNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InsuranceCompanies", "FaxNumber");
            DropColumn("dbo.InsuranceCompanies", "PhoneNumber");
            DropTable("dbo.PaRequestNotes");
            DropTable("dbo.PaRequestNoteAudits");
        }
    }
}
