using Ap_Financial_Portal.Enumerations;
using Ap_Financial_Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Ap_Financial_Portal.Helpers
{
    public class SeedHelper
    {
        private ApplicationDbContext Db { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        private readonly string DefaultAvatar, DefaultPassword, DefaultHouseName;
        private readonly char SplitCharacter;

        public SeedHelper()
        {
            Db = new ApplicationDbContext();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Db));
           
            DefaultAvatar = ReadKeyValue(Setting.DefaultAvatar);
            DefaultPassword = ReadKeyValue(Setting.DefaultPassword);
            SplitCharacter = char.Parse(ReadKeyValue(Setting.SplitCharacter));
            DefaultHouseName = ReadKeyValue(Setting.SeededHouseName);
        }

        public void SeedRoles()
        {
            foreach(var role in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {
                SeedRole(role);
            }
        }
        public void SeedHouse()
        {
            Db.Households.AddOrUpdate(
                t => t.Name,
                new Models.Household
                {
                    Name = DefaultHouseName,
                    Greeting = "This is the default greeting",
                    Created = DateTime.Now
                });
            Db.SaveChanges();
        }
        
        public void SeedUsers()
        {
            var houseId = Db.Households.AsNoTracking().FirstOrDefault(h => h.Name == DefaultHouseName).Id;
            foreach (var user in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {
                var userData = WebConfigurationManager.AppSettings[user.ToString()];
                SeedUser(userData, houseId);
            }
        }
        public void AssignRoles()
        {
            string userId = null;
            foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {
                var userData = WebConfigurationManager.AppSettings[role.ToString()];
                var userEmail = userData.Split(SplitCharacter)[0];

                userId = UserManager.FindByEmail(userEmail).Id;
                UserManager.AddToRole(userId, role.ToString());
            }
        }
        public void SeedNotification()
        {
            var headOfHouseholdEmail = ReadKeyValue(Setting.HeadofHouseholdEmail);
            var ownerId = Db.Users.FirstOrDefault(u => u.Email == headOfHouseholdEmail).Id;
            var householdId = Db.Households.AsNoTracking().FirstOrDefault(h => h.Name == DefaultHouseName).Id;
            var subject = WebConfigurationManager.AppSettings[Setting.DefaultNotificationSubject.ToString()];
            var body = WebConfigurationManager.AppSettings[Setting.DefaultNotificationBody.ToString()];
            var currentDate = DateTime.Now;

            try
            {
                Db.Notifications.AddOrUpdate(
                    n => n.Subject,
                    new Notification
                    {
                        Created = currentDate,
                        HeadOfHouseholdId = ownerId,
                        HouseholdId = householdId,
                        Subject = subject,
                        Body = body,
                        Read = false
                    });
                Db.SaveChanges();
            }
            catch(System.Exception ex)
            {
                Logger.AddException(ex);
            }
        }
        public void SeedAccountTypes()
        {
            foreach (var type in Enum.GetValues(typeof(Enumerations.AccountType)).Cast<Enumerations.AccountType>().ToList())
            {
                Db.AccountTypes.AddOrUpdate(
                    t => t.Type,
                    new Models.AccountType { Type = type.ToString(), Description = "This is a description of the" + type.ToString() + "account type."}
                    );
            }
            Db.SaveChanges();
        }
        public void SeedTransactionTypes()
        {
            foreach (var type in Enum.GetValues(typeof(Enumerations.TransactionType)).Cast<Enumerations.TransactionType>().ToList())
            {
                Db.TransactionTypes.AddOrUpdate(
                    t => t.Name,
                    new Models.TransactionType { Name = type.ToString(), Description = "This is a description of the " + type.ToString() + " transaction type." });
            }
            Db.SaveChanges();
        }
        public void SeedBudgets()
        {
            var seededHouseId = Db.Households.FirstOrDefault().Id;
            var now = DateTime.Now;
            Db.Budgets.AddOrUpdate(
                t => t.Name,
                    new Models.Budget { Name = "Household Utilities", HouseholdId = seededHouseId, Created = now, Target = 600.00 },
                    new Models.Budget { Name = "Living Expenses", HouseholdId = seededHouseId, Created = now, Target = 1000.00 },
                    new Models.Budget { Name = "Auto Maintenance", HouseholdId = seededHouseId, Created = now, Target = 500.00 },
                    new Models.Budget { Name = "Discretionary", HouseholdId = seededHouseId, Created = now, Target = 200.00 }
                );
            Db.SaveChanges();
        }
        public void SeedBudgetItems()
        {
            var budgetTypes = Db.Budgets.AsNoTracking().AsQueryable();
            var utilitiesBudgetId = budgetTypes.FirstOrDefault(b => b.Id == (int)Enumerations.Budget.HouseholdUtilities).Id;
            var livingBudgetId = budgetTypes.FirstOrDefault(b => b.Id == (int)Enumerations.Budget.LivingExpenses).Id;
            var autoBudgetId = budgetTypes.FirstOrDefault(b => b.Id == (int)Enumerations.Budget.AutoMaintenance).Id;
            var otherBudgetId = budgetTypes.FirstOrDefault(b => b.Id == (int)Enumerations.Budget.Discretionary).Id;
            var now = DateTime.Now;
            Db.BudgetItems.AddOrUpdate(
                t => t.Name,
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Gas.ToString(), BudgetId = utilitiesBudgetId, Created = now, Target = 100.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Electric.ToString(), BudgetId = utilitiesBudgetId, Created = now, Target = 100.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.WaterSewage.ToString(), BudgetId = utilitiesBudgetId, Created = now, Target = 80.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Internet.ToString(), BudgetId = utilitiesBudgetId, Created = now, Target = 100.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Fuel.ToString(), BudgetId = autoBudgetId, Created = now, Target = 200.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Repairs.ToString(), BudgetId = autoBudgetId, Created = now, Target = 200.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.RentMortgage.ToString(), BudgetId = livingBudgetId, Created = now, Target = 750.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Clothing.ToString(), BudgetId = livingBudgetId, Created = now, Target = 75.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Groceries.ToString(), BudgetId = livingBudgetId, Created = now, Target = 200.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.Entertainment.ToString(), BudgetId = otherBudgetId, Created = now, Target = 100.00 },
                new Models.BudgetItem { Name = Enumerations.BudgetItem.GymMembership.ToString(), BudgetId = otherBudgetId, Created = now, Target = 60.00 }
            );
            Db.SaveChanges();
        }
        public void SeedBankAccounts()
        {
            var houseHoldId = Db.Households.AsNoTracking().FirstOrDefault(h => h.Name == DefaultHouseName).Id;
            var ownerEmail = WebConfigurationManager.AppSettings[Setting.HeadofHouseholdEmail.ToString()];           
            var ownerId = Db.Users.FirstOrDefault(U => U.Email == ownerEmail).Id;
            var accountTypes = Db.AccountTypes.AsNoTracking().AsQueryable();
            

            Db.BankAccounts.AddOrUpdate(
                t => t.Name,
                new BankAccount
                {
                    HouseholdId = houseHoldId,
                    AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == Enumerations.AccountType.Checking.ToString()).Id,
                    OwnerId = ownerId,
                    StartingBalance = 1000.00,
                    CurrentBalance = 1000.00,
                    LowBalance = 100.00,
                    Name = "Wells Fargo",
                    Description = "Seeded Checking Account",
                    Address1 = "3375 Robinhood Road",
                    Address2 = "",
                    City = "Winston-Salem",
                    State = State.NC,
                    Zip = "27106",
                    Phone = "3367613850",
                    Created = DateTime.Now
                },
                new BankAccount
                {
                    HouseholdId = houseHoldId,
                    AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == Enumerations.AccountType.Savings.ToString()).Id,
                    OwnerId = ownerId,
                    StartingBalance = 2400.00,
                    CurrentBalance = 2400.00,
                    LowBalance = 250.00,
                    Name = "Wells Fargo",
                    Description = "Seeded Savings Account",
                    Address1 = "3375 Robinhood Road",
                    Address2 = "",
                    City = "Winston-Salem",
                    State = State.NC,
                    Zip = "27106",
                    Phone = "3367613850",
                    Created = DateTime.Now
                });
            Db.SaveChanges();
        }
        public void SeedTransactions()
        {

            var accountTypes = Db.AccountTypes.AsNoTracking().ToList();
            var budgetItemTypes = Db.BudgetItems.AsNoTracking().ToList();
            var transactionTypes = Db.TransactionTypes.AsNoTracking().ToList();
            var ownerEmail = ReadKeyValue(Setting.HeadofHouseholdEmail);
            var ownerId = UserManager.FindByEmail(ownerEmail).Id;
            var now = DateTime.Now;
            var checkBank = accountTypes.FirstOrDefault(a => a.Type == Enumerations.AccountType.Checking.ToString()).Id;
            var saveBank = accountTypes.FirstOrDefault(a => a.Type == Enumerations.AccountType.Savings.ToString()).Id;
            var withTrans = transactionTypes.FirstOrDefault(t => t.Name == Enumerations.TransactionType.Withdrawal.ToString()).Id;
            var depTrans = transactionTypes.FirstOrDefault(t => t.Name == Enumerations.TransactionType.Deposit.ToString()).Id;
            var fuel = budgetItemTypes.FirstOrDefault(a => a.Name == Enumerations.BudgetItem.Fuel.ToString()).Id;
            var rent = budgetItemTypes.FirstOrDefault(a => a.Name == Enumerations.BudgetItem.RentMortgage.ToString()).Id;
            var groceries = budgetItemTypes.FirstOrDefault(a => a.Name == Enumerations.BudgetItem.Groceries.ToString()).Id;
            Db.Transactions.AddOrUpdate(
                t => t.Memo,
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = checkBank,
                    BudgetItemId = fuel,
                    TransactionTypeId = withTrans,
                    Amount = 54.00,
                    Memo = "Two tanks of gas",
                    Created = now
                },
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = checkBank,
                    BudgetItemId = groceries,
                    TransactionTypeId = withTrans,
                    Amount = 86.00,
                    Memo = "Two weeks of groceries",
                    Created = now
                },
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = saveBank,
                    BudgetItemId = rent,
                    TransactionTypeId = withTrans,
                    Amount = 750.00,
                    Memo = "One month's rent",
                    Created = now
                },
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = saveBank,
                    BudgetItemId = null,
                    TransactionTypeId = depTrans,
                    Amount = 500.00,
                    Memo = "Direct deposit",
                    Created = now
                });
            Db.SaveChanges();
        }


        #region Private
        private string ReadKeyValue(Setting settings)
        {
            return WebConfigurationManager.AppSettings[settings.ToString()];
        }
        private void SeedUser(string userData, int houseId)
        {
            var data = userData.Split(SplitCharacter);
            var email = data[0];
            try
            {
                if (!Db.Users.Any(u => u.Email == email))
                {
                    UserManager.Create(new ApplicationUser
                    {
                        HouseholdId = houseId,
                        Email = email,
                        UserName = email,
                        FirstName = data[1],
                        LastName = data[2],
                        DisplayName = data[3],
                        AvatarUrl = DefaultAvatar
                    }, DefaultPassword);
                }
            }
            catch (System.Exception ex)
            {
                Logger.AddException(ex);
            }
        }
        private void SeedRole(Role role)
        {
            var roleName = role.ToString();
            if (!Db.Roles.Any(r => r.Name == roleName))
            {
                RoleManager.Create(new IdentityRole { Name = roleName });
            }
        }
        #endregion
    }
}