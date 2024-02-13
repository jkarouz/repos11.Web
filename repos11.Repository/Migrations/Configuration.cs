using System.Data.Entity.Migrations;

namespace repos11.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Entity.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Entity.ApplicationDbContext context)
        {
            SeedData.InitialUserManagement(context);
        }
    }
}
