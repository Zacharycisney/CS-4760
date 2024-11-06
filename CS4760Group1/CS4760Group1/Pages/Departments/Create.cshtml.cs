using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;
using CS4760Group1.Models;

namespace CS4760Group1.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        public CreateModel(CS4760Group1Context context)
        {
            _context = context;
        }
        public List<SelectListItem> UserList { get; set; }

        [BindProperty]
        public int SelectedCollegeId { get; set; }

        public List<SelectListItem> CollegeList { get; set; }

        [BindProperty]
        public Department Department { get; set; } = new Department();


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

        public IActionResult OnGet()
        {
            // Fetch the list of colleges from the database
            CollegeList = _context.College
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            PopulateUserList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            PopulateUserList();

            if (!ModelState.IsValid)
            {
                PopulateUserList();

                // Reload the colleges list if validation fails
                CollegeList = _context.College
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList();

                return Page();
            }

            // Set the selected CollegeID for the new department
            Department.CollegeID = SelectedCollegeId;

            _context.Department.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
