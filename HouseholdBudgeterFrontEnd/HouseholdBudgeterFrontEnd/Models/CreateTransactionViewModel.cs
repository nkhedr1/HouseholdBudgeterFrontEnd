using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class CreateTransactionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public int HouseholdId { get; set; }
        public int CategoryId { get; set; }
        public bool VoidTransaction { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}