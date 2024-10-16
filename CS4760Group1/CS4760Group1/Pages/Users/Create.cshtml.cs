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
            UserAffiliation = new UserAffiliation(); // Initialize the UserAffiliation instance
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public UserAffiliation UserAffiliation { get; set; }

        public List<SelectListItem> CollegeList { get; set; }
        [BindProperty]
        public int SelectedCollegeId { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();

        public void OnGet()
        {
            setColleges();
            Roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        }

        /// <summary>
        /// Creates a list of possible colleges
        /// </summary>
        public void setColleges()
        {
            CollegeList = _context.College
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

        /// <summary>
        /// Creates a list of possible departments based on the selected college
        /// </summary>
        public void setDepartments(int selectedCollegeId)
        {
            if (selectedCollegeId != 0)
            {
                DepartmentList = _context.Department
                    .Where(d => d.CollegeID == selectedCollegeId)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name
                    })
                    .ToList();
            }
        }

        public JsonResult OnGetDepartments(int collegeId)
        {
            var departments = _context.Department
                .Where(d => d.CollegeID == collegeId)
                .Select(d => new { value = d.Id, text = d.Name })
                .ToList();

            return new JsonResult(departments);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList(); // Ensure roles are populated again on invalid state
                return Page();
            }

            // Save the user first to generate the User ID
            _context.Users.Add(User);
            await _context.SaveChangesAsync(); // Save and generate User.Id

            // Now you can set UserAffiliation properties
            UserAffiliation.UserId = User.Id; // Set the generated User ID
            UserAffiliation.CollegeId = SelectedCollegeId; // This should be set from your form input

            //_context.UserAffiliations.Add(UserAffiliation);
            await _context.SaveChangesAsync(); // Save the UserAffiliation

            return RedirectToPage("./Index");
        }
    }
}
