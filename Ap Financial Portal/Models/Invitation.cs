using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(40)]
        public string Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Subject { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Body { get; set; }
        public DateTime Created { get; set; }

        [Display(Name = "Time to live")]
        public int TTL { get; set; }
        public bool IsValid { get; set; }
        public Guid Code { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}