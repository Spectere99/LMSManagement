namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Security_v14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLoginAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        RecordId = c.Int(nullable: false),
                        Login = c.String(),
                        PasswordHash = c.String(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTime(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        RefreshId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLoginAudits");
        }
    }
}
