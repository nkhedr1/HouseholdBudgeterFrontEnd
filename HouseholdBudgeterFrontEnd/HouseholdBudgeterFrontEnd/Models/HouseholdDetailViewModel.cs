using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class HouseholdDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public List<BankAccountViewModel> BankAccounts { get; set; }
        public List<CategoryDetailViewModel> HouseholdCategories { get; set; }
        public decimal TotalBalance { get; set; }
    }
}