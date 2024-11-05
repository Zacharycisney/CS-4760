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
            //UserAffiliation = new UserAffiliation(); // Initialize the UserAffiliation instance
        }

        public List<SelectListItem> GrantList { get; set; } // Grant List
        [BindProperty]
        public int SelectedGrantId { get; set; } // Binding selected grant


        public List<College> Colleges { get; set; } = new List<College>();
        public List<Department> Departments { get; set; } = new List<Department>();

        //public List<SelectListItem> CollegeList { get; set; }
        //[BindProperty]
        //public int SelectedCollegeId { get; set; }

        //public List<SelectListItem> DepartmentList { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();


        /// <summary>
        /// set the grants combo box
        /// </summary>
        private void setGrants()
        {
            GrantList = _context.Grant
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Title
                })
                .ToList();
        }


        /// <summary>
        /// Creates a list of possible colleges
        /// </summary>
        //public void setCollegesCombo()
        //{
        //    CollegeList = _context.College
        //        .Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name
        //        })
        //        .ToList();
        //}

        /// <summary>
        /// Creates a list of possible departments based on the selected college
        /// </summary>
        //public void setDepartmentsCombo(int selectedCollegeId)
        //{
        //    if (selectedCollegeId != 0)
        //    {
        //        DepartmentList = _context.Department
        //            .Where(d => d.CollegeID == selectedCollegeId)
        //            .Select(d => new SelectListItem
        //            {
        //                Value = d.Id.ToString(),
        //                Text = d.Name
        //            })
        //            .ToList();
        //    }
        //}

        //public JsonResult OnGetDepartments(int collegeId)
        //{
        //    var departments = _context.Department
        //        .Where(d => d.CollegeID == collegeId)
        //        .Select(d => new { value = d.Id, text = d.Name })
        //        .ToList();

        //    return new JsonResult(departments);
        //}


        public IList<User> User { get; set; } = default!;


        public class GrantReviewInputModel
        {
            public int GrantId { get; set; } // Grant ID
            public List<int> UserIds { get; set; } // List of user IDs
        }

        public async Task<IActionResult> OnPostSaveCommitteeMembers([FromBody] GrantReviewInputModel input)
        {
            if (input.GrantId == 0 || input.UserIds == null || input.UserIds.Count == 0)
            {
                return new JsonResult(new { success = false, message = "Invalid input" });
            }

            // Create GrantReview entries for each committee member
            foreach (var userId in input.UserIds)
            {
                var grantReview = new GrantReview
                {
                    GrantID = input.GrantId,
                    UserID = userId,
                    Status = "Not Reviewed",
                    ReviewScore = 0 // Initially null
                };

                _context.GrantReview.Add(grantReview);
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }



        public async Task OnGetAsync() // add data base retrival code
        {
            setGrants();
            //setCollegesCombo();

            // Load colleges and departments for lookups
            Colleges = await _context.College.ToListAsync();
            Departments = await _context.Department.ToListAsync();

            User = await _context.Users.ToListAsync();
        }
    }
}
