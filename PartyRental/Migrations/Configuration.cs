namespace PartyRental.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PartyRental.RentalData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PartyRental.RentalData";
        }

        protected override void Seed(PartyRental.RentalData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
