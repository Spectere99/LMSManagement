namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequests", "FileUploadLogId", c => c.Int(nullable: false));
            AddColumn("dbo.PaRequests", "BatchName", c => c.String());
            CreateIndex("dbo.PaRequests", "FileUploadLogId");
            AddForeignKey("dbo.PaRequests", "FileUploadLogId", "dbo.FileUploadLogs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaRequests", "FileUploadLogId", "dbo.FileUploadLogs");
            DropIndex("dbo.PaRequests", new[] { "FileUploadLogId" });
            DropColumn("dbo.PaRequests", "BatchName");
            DropColumn("dbo.PaRequests", "FileUploadLogId");
        }
    }
}
