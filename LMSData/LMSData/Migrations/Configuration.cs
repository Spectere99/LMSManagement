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

            if (!context.Roles.Any())
            {
                IList<Role> defaultRoles = new List<Role>();

                defaultRoles.Add(new Role()
                {
                    Id = 1, LastModifiedBy = "INSTALL", CreatedBy = "INSTALL", Created = DateTime.Now,
                    LastModified = DateTime.Now, Archived = false, Name = "Administrator"
                });
                defaultRoles.Add(new Role()
                {
                    Id = 2, LastModifiedBy = "INSTALL", CreatedBy = "INSTALL", Created = DateTime.Now,
                    LastModified = DateTime.Now, Archived = false, Name = "Lead"
                });
                defaultRoles.Add(new Role()
                {
                    Id = 3, LastModifiedBy = "INSTALL", CreatedBy = "INSTALL", Created = DateTime.Now,
                    LastModified = DateTime.Now, Archived = false, Name = "Standard User"
                });

                context.Roles.AddRange(defaultRoles);
            }

            if (!context.LookupTypes.Any())
            {
                IList<LookupType> defaultLookupTypes = new List<LookupType>();

                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 1, Type = "Roles"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 2, Type = "System"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 3, Type = "Modules"});
                defaultLookupTypes.Add(new LookupType() {Archived = false, Id = 4, Type = "Pa_RequestStatus"});
                defaultLookupTypes.Add(new LookupType() { Archived = false, Id = 5, Type = "BillingStatus" });

                context.LookupTypes.AddRange(defaultLookupTypes);
            }

            if (!context.Lookups.Any())
            {
                IList<Lookup> defaultLookups = new List<Lookup>();

                defaultLookups.Add(new Lookup() { Archived = false, Id = 0, LookupTypeId = 1, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "User"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 1, LookupTypeId = 1, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Admin"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 2, LookupTypeId = 3, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Pa_Requests"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 3, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Assigned"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 4, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Submitted"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 5, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "On Hold"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 6, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Approved"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 7, LookupTypeId = 4, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Denied"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 5, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Ready to Invoice"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 5, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Invoiced"});
                defaultLookups.Add(new Lookup() { Archived = false, Id = 8, LookupTypeId = 5, Created = DateTime.Now,
                                      CreatedBy = "INSTALL", LastModified = DateTime.Now, LastModifiedBy = "INSTALL", LookupValue = "Paid"});
                context.Lookups.AddRange(defaultLookups);
            }

            if (!context.Users.Any())
            {
                IList<User> defaultUsers = new List<User>();

                defaultUsers.Add(new User()
                {
                    Archived = false, Id = 1, Created = DateTime.Now, CreatedBy = "INSTALL", LastModifiedBy = "INSTALL",
                    LastModified = DateTime.Now, Email = "rflowers@saber98.com", FirstName = "System", LastName = "Administrator",
                    PhoneNumber = "", UserName = "admin", RoleId = 1
                });

                context.Users.AddRange(defaultUsers);
            }

            if (!context.UserLogins.Any())
            {
                IList<UserLogin> defaultUserLogins = new List<UserLogin>();

                defaultUserLogins.Add(new UserLogin()
                    {
                        Id = 1, LastModifiedBy = "INSTALL", CreatedBy = "INSTALL", Created = DateTime.Now,
                        LastModified = DateTime.Now, AccessFailedCount = 0, IsAdmin = true,
                        LockoutEnabled = false, LockoutEnd = DateTime.Parse("1/1/1900"),
                        PasswordHash = @"4A82C7AAC9F064913EFF3F1286D74B56463F2D2B0C8DE117126E8D1723CB2873", // SHA256 for pwd (Saber98)
                    Login = "admin"
                });

                context.UserLogins.AddRange(defaultUserLogins);
            }
            base.Seed(context);
        }
    }
}
