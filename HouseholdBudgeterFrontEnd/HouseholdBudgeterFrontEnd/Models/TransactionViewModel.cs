using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public decimal Amount { get; set; }
        public bool VoidTransaction { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public int HouseholdId { get; set; }
        public int CategoryId { get; set; }

    }
}