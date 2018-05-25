using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using LIMSData.DBObjects;

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

            if (!context.LookupTypes.Any())
            {
                IList<LookupType> defaultLookupTypes = new List<LookupType>();

                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 0, Type = "Roles"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 1, Type = "System"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 2, Type = "Modules"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 3, Type = "Pa_RequestStatus"});
                defaultLookupTypes.Add(new LookupType() { Archived = false, Id = 4, Type = "BillingStatus" });

                context.LookupTypes.AddRange(defaultLookupTypes);
            }

            if (!context.Lookups.Any())
            {
                IList<Lookup> defaultLookups = new List<Lookup>();

                defaultLookups.Add(new Lookup() { Archived = false, Id = 0, LookupTypeId = 0, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "User"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 1, LookupTypeId = 0, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Admin"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 2, LookupTypeId = 2, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Pa_Requests"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 3, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Assigned"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 4, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Submitted"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 5, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "On Hold"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 6, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Approved"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 7, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Denied"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Ready to Invoice"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Invoiced"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "SYSTEM", LastModified = DateTime.Now, LastModifiedBy = "SYSTEM", LookupValue = "Paid"});
                context.Lookups.AddRange(defaultLookups);
            }
            base.Seed(context);
        }
    }
}
