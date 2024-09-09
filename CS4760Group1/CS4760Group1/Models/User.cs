using System.ComponentModel.DataAnnotations;
using System.Data;
namespace CS4760Group1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Role Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    // limits allowed roles
    public enum Role
    {
        Admin,
        Chair,
        Tenured,    // fully tenured
        Tenure,     // on track for tenure
        Instructor,
        Adjunct,
        Staff
    }
}
