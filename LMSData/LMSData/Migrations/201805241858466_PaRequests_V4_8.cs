namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequests_V4_8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lookups", "CreatedBy", c => c.String());
            AlterColumn("dbo.Lookups", "LastModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lookups", "LastModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Lookups", "CreatedBy", c => c.Int(nullable: false));
        }
    }
}
