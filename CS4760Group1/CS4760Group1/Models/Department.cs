using System.ComponentModel.DataAnnotations;
namespace CS4760Group1.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Dean { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string? Location { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? College { get; set; }
    }
}
