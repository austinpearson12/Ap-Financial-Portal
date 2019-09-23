using Ap_Financial_Portal.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int AccountTypeId { get; set; }
        public string OwnerId { get; set; }
        public double StartingBalance { get; set; }
        public double CurrentBalance { get; set; }
        public double LowBalance { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Description { get; set; }
        [StringLength(40)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        public State State { get; set; }
        [StringLength(10)]
        public string Zip { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

    }
}