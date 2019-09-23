using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Enumerations
{
    public enum Budget
    {
        [Display(Name = "Household Utilities")]
        HouseholdUtilities = 1,
        [Display(Name = "Living Expenses")]
        LivingExpenses,
        [Display(Name = "Auto Maintenance")]
        AutoMaintenance,
        [Display(Name = "Discretionary")]
        Discretionary
    }
}