using System.Data.Entity.Migrations;
using EmployeeSurvey.Web.Models;

namespace EmployeeSurvey.Web.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EmployeeSurvey.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //comment by Phuc
            context.Users.AddOrUpdate(
                x => x.UserName,
                new ApplicationUser()
                {
                    UserName = "Admin",
                    FullName = "Admin",
                    PasswordHash = "AMOUnOhWXHc+FkaLlwgut/2+cg5DUJYayjKDRF82Pvdg8IAVIWWuH95Ox6XSSn/fEA==",
                    PasswordBeforeHash = "_Admin123!@#",
                    EmployeeId = "Admin",
                    SecurityStamp = "7b0507cd-5078-4ddb-97d6-055223b85bdd",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    Country = "ZPVN",
                    Level = "99"
                }
            );
        }
    }
}
