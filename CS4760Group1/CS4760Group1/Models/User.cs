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

        
        // Constructor fixes errors related to non-nullable words
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
