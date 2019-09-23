using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Name { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }
    }
}