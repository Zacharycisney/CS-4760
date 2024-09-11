using System.ComponentModel.DataAnnotations;
using System.Data;
namespace CS4760Group1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // Parameterless constructor
        public User() { }

        // Constructor for initial validation
        public User(string userName, string password, string firstName, string lastName, Role role, string email)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Role = role;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
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
