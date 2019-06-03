using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class InviteUser
    {
        
        public int Id { get; set; }

        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}