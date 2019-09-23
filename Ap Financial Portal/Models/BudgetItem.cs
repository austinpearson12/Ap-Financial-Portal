using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        [StringLength(40, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        public double Target { get; set; }
        public double Actual { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Budget Budget { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}