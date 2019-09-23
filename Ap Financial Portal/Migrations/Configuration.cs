namespace Ap_Financial_Portal.Migrations
{
    using Ap_Financial_Portal.Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ap_Financial_Portal.Models.ApplicationDbContext>
    {
        private SeedHelper seedHelper = new SeedHelper();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ap_Financial_Portal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            seedHelper.SeedRoles();
            seedHelper.SeedHouse();
            seedHelper.SeedUsers();
            seedHelper.AssignRoles();
            seedHelper.SeedAccountTypes();
            seedHelper.SeedBankAccounts();
            seedHelper.SeedTransactionTypes();
            seedHelper.SeedBudgets();
            seedHelper.SeedBudgetItems();
            seedHelper.SeedNotification();
            seedHelper.SeedTransactions();

        }
    }
}
