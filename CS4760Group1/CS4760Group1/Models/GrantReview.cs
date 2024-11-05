using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantReview
    {
        public int ID { get; set; }

        [Required]
        public int GrantID { get; set; }  // Foreign key to the Grant table

        [Required]
        public int UserID { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public float ReviewScore { get; set; }
    }
}