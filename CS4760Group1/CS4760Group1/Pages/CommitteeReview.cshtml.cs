using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CS4760Group1.Pages
{
    public class CommitteeReviewModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        public CommitteeReviewModel(CS4760Group1Context context)
        {
            _context = context;
            UserAffiliation = new UserAffiliation(); // Initialize the UserAffiliation instance
        }


        [BindProperty]
        public UserAffiliation UserAffiliation { get; set; }

        public List<SelectListItem> CollegeList { get; set; }
        [BindProperty]
        public int SelectedCollegeId { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();


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


        public IList<User> User { get; set; } = default!;




        public async Task OnGetAsync() // add data base retrival code
        {
            setColleges();
            User = await _context.Users.ToListAsync();
        }
    }
}
