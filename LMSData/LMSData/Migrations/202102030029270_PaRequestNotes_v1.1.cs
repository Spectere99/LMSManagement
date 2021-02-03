namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequestNotes_v11 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PaRequestNotes", "PaRequestId");
            AddForeignKey("dbo.PaRequestNotes", "PaRequestId", "dbo.PaRequests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaRequestNotes", "PaRequestId", "dbo.PaRequests");
            DropIndex("dbo.PaRequestNotes", new[] { "PaRequestId" });
        }
    }
}
