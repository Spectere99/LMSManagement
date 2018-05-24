namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileUploadLogs", "BatchName", c => c.String());
            DropColumn("dbo.PaRequests", "BatchName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaRequests", "BatchName", c => c.String());
            DropColumn("dbo.FileUploadLogs", "BatchName");
        }
    }
}
