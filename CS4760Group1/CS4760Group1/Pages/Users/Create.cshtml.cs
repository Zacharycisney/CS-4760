using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;
using CS4760Group1.Models;

namespace CS4760Group1.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        public CreateModel(CS4760Group1Context context)
        {
            _context = context;
            User = new User(); // Initialize the User instance
        }

        [BindProperty]
        public User User { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();

        public void OnGet()
        {
            Roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList(); // Ensure roles are populated again on invalid state
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
