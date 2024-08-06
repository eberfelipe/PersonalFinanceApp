using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; 

        [Required]
        public decimal Value { get; set; }

        [Required]
        public ExpenseCategory Category { get; set; }
    }
}
