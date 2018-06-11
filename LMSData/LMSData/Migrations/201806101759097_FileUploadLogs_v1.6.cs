namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileUploadLogs_v16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileUploadLogs", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileUploadLogs", "Archived");
        }
    }
}
