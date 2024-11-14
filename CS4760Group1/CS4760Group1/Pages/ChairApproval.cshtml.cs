using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS4760Group1.Pages
{
    public class ChairApprovalModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public ChairApprovalModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
        }

        public IList<Grant> Grant { get; set; } = default!;
        public List<SelectListItem> DepartmentList { get; set; }
        public int SelectedDepartmentId { get; set; } // Holds the selected department ID

        public Dictionary<int, double> ApprovalRatings { get; set; } = new Dictionary<int, double>();

        public void SetDepartments()
        {
            DepartmentList = _context.Department
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

        public async Task OnGetAsync(int? selectedDepartmentId)
        {
            SetDepartments();

            // Filter grants by selected department if provided
            var query = _context.Grant.AsQueryable();
            if (selectedDepartmentId.HasValue)
            {
                query = query.Where(g => g.DepartmentID == selectedDepartmentId.Value);
                SelectedDepartmentId = selectedDepartmentId.Value;
            }

            Grant = await query.ToListAsync();

            // Calculate approval rating for each grant
            foreach (var grant in Grant)
            {
                //var reviews = await _context.GrantReview
                //    .Where(gr => gr.GrantID == grant.Id)
                //    .ToListAsync();

                //if (reviews.Any())
                //{
                //    float totalScore = reviews.Sum(r => r.ReviewScore);
                //    float totalReviewers = reviews.Count;
                //    ApprovalRatings[grant.Id] = (double)totalScore / totalReviewers;
                //}
                //else
                {
                    ApprovalRatings[grant.Id] = 0; // No reviews, set rating to 0
                }
            }
        }
    }
}
