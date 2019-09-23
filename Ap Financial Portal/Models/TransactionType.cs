using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}