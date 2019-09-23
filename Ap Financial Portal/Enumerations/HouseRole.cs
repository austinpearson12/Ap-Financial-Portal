using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Enumerations
{
    public enum HouseRole
    {
        [Display(Name = "Head of House")]
        HeadOfHouse,
        Resident,
        Lobbyist,
        Admin
    }
}