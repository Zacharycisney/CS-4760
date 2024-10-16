using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; }


    }

    public enum Role
    {
        Staff,
        Admin,
        Chair,
        Tenured,
        Tenure,
        Instructor,
        Adjunct
    }

}
