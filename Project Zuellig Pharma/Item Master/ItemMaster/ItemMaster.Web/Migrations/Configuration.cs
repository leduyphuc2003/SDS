using System.Data.Entity.Migrations;
using ItemMaster.Web.Models;

namespace ItemMaster.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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
            context.Users.AddOrUpdate(
                x => x.UserName,
                new ApplicationUser()
                {
                    UserName = "Admin",
                    PasswordHash = "AMOUnOhWXHc+FkaLlwgut/2+cg5DUJYayjKDRF82Pvdg8IAVIWWuH95Ox6XSSn/fEA==",
                    //PasswordBeforeHash = "_Admin123!@#",
                    SecurityStamp = "7b0507cd-5078-4ddb-97d6-055223b85bdd",
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }
    }
}
