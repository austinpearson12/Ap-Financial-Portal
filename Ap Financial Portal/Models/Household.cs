﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Greeting { get; set; }
        public DateTime Created { get; set; }


        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public Household()
        {
            Members = new HashSet<ApplicationUser>();
            BankAccounts = new HashSet<BankAccount>();
            Budgets = new HashSet<Budget>();
            Notifications = new HashSet<Notification>();
            Invitations = new HashSet<Invitation>();
        }
    }
}