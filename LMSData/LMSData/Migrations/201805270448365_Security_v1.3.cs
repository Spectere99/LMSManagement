namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Security_v13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogins", "RefreshId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogins", "RefreshId");
        }
    }
}
