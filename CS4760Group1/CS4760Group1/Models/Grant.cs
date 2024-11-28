using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class Grant : GrantBase
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? PI { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public decimal Amount { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public string? SubType { get; set; }

        [Required]
        public string? Season { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public decimal AmountFromOther { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int CollegeID { get; set; } //FK to college table

        [Required]
        public int DepartmentID { get; set; } //FK to dept table

        [Required]
        public string? ProcMethod { get; set; }

        [Required]
        public string? Timeline { get; set; }

        [RegularExpression(@"^(?!0(\.0+)?$)([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$"), Required]
        public int Index { get; set; }

        [Required]
        public bool SubjectNeeded { get; set; }
    }
}
