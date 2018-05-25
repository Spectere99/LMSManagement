namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaRequestAudits", "CompletedTimeStamp", c => c.DateTime());
            AlterColumn("dbo.PaRequests", "CompletedTimeStamp", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaRequests", "CompletedTimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaRequestAudits", "CompletedTimeStamp", c => c.DateTime(nullable: false));
        }
    }
}
