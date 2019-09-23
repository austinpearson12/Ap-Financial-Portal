using Ap_Financial_Portal.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public int? BudgetItemId { get; set; }
        public string OwnerId { get; set; }
        public double Amount { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Memo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }


        public virtual BankAccount BankAccount { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }

    }
}