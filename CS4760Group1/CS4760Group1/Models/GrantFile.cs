using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantFile
    {
        //[Key]
        public int Id { get; set; }  // This is auto-increment by default with EF

        [Required]
        public int GrantID { get; set; }  // Foreign key to the Grant table

        [Required]
        public string FileName { get; set; }  // Name of the uploaded file

        [Required]
        public string FilePath { get; set; }  // Path where the file is stored

        public Grant Grant { get; set; }  // Navigation property
    }
}
