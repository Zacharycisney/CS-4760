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

        [RegularExpression(@"^[1-9][0-9]*$"), Required]
        public int AmountRequested { get; set; }
    }
}
