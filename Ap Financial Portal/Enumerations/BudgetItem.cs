using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Enumerations
{
    public enum BudgetItem
    {
        [Display(Name = "Gas")]
        Gas = 1,
        [Display(Name = "Electric")]
        Electric,
        [Display(Name = "Water/Sewage")]
        WaterSewage,
        [Display(Name = "Internet")]
        Internet,
        [Display(Name = "Fuel")]
        Fuel,
        [Display(Name = "Repairs")]
        Repairs,
        [Display(Name = "Rent/Mortgage")]
        RentMortgage,
        [Display(Name = "Clothing")]
        Clothing,
        [Display(Name = "Groceries")]
        Groceries,
        [Display(Name = "Entertainment")]
        Entertainment,
        [Display(Name = "Gym Membership")]
        GymMembership
    }
}