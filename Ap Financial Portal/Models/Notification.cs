using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string HeadOfHouseholdId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Subject { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool Read { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser OwnerId { get; set; }
    }
}