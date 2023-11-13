using SupriMaster.Infra.Data.Content;
using System.Data.Entity.Migrations;

namespace SupriMaster.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SupriMasterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

    }
}
