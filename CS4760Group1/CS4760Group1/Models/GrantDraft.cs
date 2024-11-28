using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantDraft : GrantBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? PI { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal Amount { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal AmountFromOther { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal TotalAmount { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public int Index { get; set; }

    }
}
