using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public AccountType()
        {
            BankAccounts = new HashSet<BankAccount>();
        }
    }
}