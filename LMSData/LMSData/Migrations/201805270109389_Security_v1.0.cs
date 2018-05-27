namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Security_v10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogins", "Login", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogins", "Login");
        }
    }
}
