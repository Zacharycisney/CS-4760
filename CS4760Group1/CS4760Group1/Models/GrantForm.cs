using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantForm
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? PI { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? GrantType { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public decimal AmountRequested { get; set; }

        [Required]
        public string? GrantStatus { get; set; }
    }
}
