using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantDraft
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? PI { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal Amount { get; set; }

        public string? Status { get; set; }

        public string? SubType { get; set; }

        public string? Season { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal AmountFromOther { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public decimal TotalAmount { get; set; }

        public int CollegeID { get; set; } //FK to college table

        public int DepartmentID { get; set; } //FK to dept table

        public string? ProcMethod { get; set; }

        public string? Timeline { get; set; }

        [RegularExpression(@"^([1-9][0-9]{0,16}|0)(\.[0-9]{1,2})?$")]
        public int Index { get; set; }

        public bool SubjectNeeded { get; set; }
    }
}
