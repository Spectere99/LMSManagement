namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_v15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserLoginId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "UserLoginId");
            AddForeignKey("dbo.Users", "UserLoginId", "dbo.UserLogins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserLoginId", "dbo.UserLogins");
            DropIndex("dbo.Users", new[] { "UserLoginId" });
            DropColumn("dbo.Users", "UserLoginId");
        }
    }
}
