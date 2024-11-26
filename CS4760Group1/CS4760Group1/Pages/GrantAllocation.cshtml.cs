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
    public class GrantAllocationModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public GrantAllocationModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
        }

        public IList<Grant> Grant { get; set; } = default!;
        public List<SelectListItem> DepartmentList { get; set; }
        public int SelectedDepartmentId { get; set; } // Holds the selected department ID
        public Dictionary<int, double> ApprovalRatings { get; set; } = new Dictionary<int, double>();


        /// <summary>
        /// Retrieves the departments for combo box
        /// </summary>
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

            await getApprovalRatingsAsync();

            // Sort grants by approval ratings (highest to lowest)
            Grant = Grant.OrderByDescending(g => ApprovalRatings.ContainsKey(g.Id) ? ApprovalRatings[g.Id] : 0).ToList();
        }

        /// <summary>
        /// Does the math on all the grants as well as check for blank grant list
        /// </summary>
        /// <returns></returns>
        public async Task getApprovalRatingsAsync()
        {
            // Prevents race conditions when grants is not populated when filtering
            if (Grant == null || !Grant.Any())
            {
                ApprovalRatings.Clear();
                return;
            }

            // Querying the data
            var reviewStatsQuery = _context.GrantReview
                .GroupBy(gr => gr.GrantID)
                .Select(group => new
                {
                    GrantId = group.Key,
                    ApprovalRating = group.Average(gr => gr.ReviewScore),
                    ReviewCount = group.Count()
                });

            var reviewStats = await reviewStatsQuery.ToDictionaryAsync(x => x.GrantId, x => new { x.ApprovalRating, x.ReviewCount });

            // Doing the math on the review scores
            foreach (var grant in Grant)
            {
                if (reviewStats.TryGetValue(grant.Id, out var stats))
                {
                    // NOTE: The score is currently based on a whole number that has a MAX of 54
                    // (the math takes the total # of reviews and then multiplies by max score and then divides the total score)
                    ApprovalRatings[grant.Id] = stats.ApprovalRating / (stats.ReviewCount * 54);

                }
                else // If there are no reviews
                {
                    ApprovalRatings[grant.Id] = 0;
                }
            }
        }


        /// <summary>
        /// Approval function for approve button
        /// </summary>
        /// <param name="grantId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostApproveGrantAsync(int grantId)
        {
            // Catching invalid grants

            var grant = await _context.Grant.FindAsync(grantId);
            if (grant == null)
            {
                return NotFound();
            }

            // Update the status to "Approved"
            grant.Status = "Approved";
            await _context.SaveChangesAsync();

            return RedirectToPage(); // Refresh the page
        }

        /// <summary>
        /// Denial function for deny button
        /// </summary>
        /// <param name="grantId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDenyGrantAsync(int grantId)
        {
            // Catching invalid grants
            var grant = await _context.Grant.FindAsync(grantId);
            if (grant == null)
            {
                return NotFound();
            }

            // Update the status to "Denied"
            grant.Status = "Denied";
            await _context.SaveChangesAsync();

            return RedirectToPage(); // Refresh the page
        }

    }
}
