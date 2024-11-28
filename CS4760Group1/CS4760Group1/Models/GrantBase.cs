namespace CS4760Group1.Models
{
    public abstract class GrantBase
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? PI { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public decimal Amount { get; set; }

        public string? Status { get; set; }

        public string? SubType { get; set; }

        public string? Season { get; set; }

        public decimal AmountFromOther { get; set; }

        public decimal TotalAmount { get; set; }

        public int CollegeID { get; set; }

        public int DepartmentID { get; set; }

        public string? ProcMethod { get; set; }

        public string? Timeline { get; set; }

        public int Index { get; set; }

        public bool SubjectNeeded { get; set; }
    }
}
