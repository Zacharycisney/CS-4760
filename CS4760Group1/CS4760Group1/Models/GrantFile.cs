namespace CS4760Group1.Models
{
    public class GrantFile
    {
        public int Id { get; set; } // Primary key
        public int GrantId { get; set; } // Foreign key to the Grant table
        public string FileName { get; set; }
        public string FilePath { get; set; }

        // Navigation property (optional) to relate to the Grant entity
        public Grant Grant { get; set; }
    }

}
