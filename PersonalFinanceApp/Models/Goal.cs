using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        public ExpenseCategory Category { get; set; }

        [Required]
        public decimal GoalAmount { get; set; }

        public decimal SpentAmount { get; set; }

        public decimal Balance { get; set; }
    }
}
