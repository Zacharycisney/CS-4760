using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;
using CS4760Group1.Models;

namespace CS4760Group1.Pages.Colleges
{
    public class CreateModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public CreateModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUserList();

            return Page();
        }

        public List<SelectListItem> UserList { get; set; }


        [BindProperty]
        public College College { get; set; } = default!;

        private void PopulateUserList()
        {
            UserList = _context.Users
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                })
                .ToList();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            PopulateUserList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.College.Add(College);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
