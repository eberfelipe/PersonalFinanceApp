using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApp.Models
{
    public class Income
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty ;
    }
}
