using System.Data.Entity.Migrations;

namespace LIMSData.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LIMSData.LMSDataDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LIMSData.LMSDataDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
