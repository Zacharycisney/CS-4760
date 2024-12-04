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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CS4760Group1.Pages
{
    public class GrantAllocationModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        public GrantAllocationModel(CS4760Group1Context context)
        {
            _context = context;
        }

        public IList<Grant> Grant { get; set; } = new List<Grant>();
        public List<SelectListItem> DepartmentList { get; set; }
        public int? SelectedDepartmentId { get; set; }
        public decimal? SelectedDepartmentAllowance { get; set; }
        public decimal RemainingAllowance { get; set; }
        public Dictionary<int, double> ApprovalRatings { get; set; } = new Dictionary<int, double>();
        public double? MinAvgRating { get; set; }
        public string ErrorMessage { get; set; }

        public void SetDepartments()
        {
            DepartmentList = _context.Department
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToList();
        }

        public async Task OnGetAsync(int? selectedDepartmentId, double? minAvgRating = null)
        {
            SetDepartments();
            var query = _context.Grant.AsQueryable();
            Console.Write("\non get called\n");

            if (selectedDepartmentId.HasValue)
            {
                Console.Write("\ndepartmentId has value\n");
                query = query.Where(g => g.DepartmentID == selectedDepartmentId.Value);
                SelectedDepartmentId = selectedDepartmentId;
                Console.Write("\ndepartmentId = " , SelectedDepartmentId.Value);

                SelectedDepartmentAllowance = await _context.Department
                    .Where(d => d.Id == selectedDepartmentId.Value)
                    .Select(d => d.Allowance)
                    .FirstOrDefaultAsync();

                RemainingAllowance = SelectedDepartmentAllowance ?? 0;
            }

            if (minAvgRating.HasValue)
            {
                Console.Write("\nminAvgRating has value\n");
                // Normalize the minimum average rating to the 0–54 scale
                MinAvgRating = (minAvgRating / 100.0);
                Console.Write("\nminAvgRating = ", MinAvgRating.Value.ToString());

                // Include grants with sufficient approval ratings
                query = query.Where(g =>
                    _context.GrantReview
                        .Where(r => r.GrantID == g.Id)
                        .GroupBy(r => r.GrantID)
                        .Select(grp => grp.Average(r => r.ReviewScore))
                        .FirstOrDefault() >= (MinAvgRating * 54))
                       .OrderByDescending(g =>
                    _context.GrantReview
                        .Where(r => r.GrantID == g.Id)
                        .GroupBy(r => r.GrantID)
                        .Select(grp => grp.Average(r => r.ReviewScore))
                        .FirstOrDefault()); ;
            }

            Grant = await query.Where(g => g.Status == "Approved").ToListAsync();

            //foreach (var grant in Grant)
            //{
            //    Console.WriteLine($"\nGrant ID: {grant.Id}, Title: {grant.Title}, Amount: {grant.Amount}, TotalAmount: {grant.TotalAmount}\n");
            //}
            await GetApprovalRatingsAsync();
        }

        public async Task<IActionResult> OnPostApplyRangesAsync(List<RangeDto> ranges, int? selectedDepartmentId, double? minAvgRating)
        {
            Console.Write("\nOn Post got called\n");

            if (!ranges.Any())
            {
                ErrorMessage = "No ranges provided.";
                Console.WriteLine($"\n{ErrorMessage}\n");
                return RedirectToPage();
            }

            // Reapply filters from OnGetAsync
            var query = _context.Grant.AsQueryable();

            if (selectedDepartmentId.HasValue)
            {
                query = query.Where(g => g.DepartmentID == selectedDepartmentId.Value);
                SelectedDepartmentId = selectedDepartmentId;

                SelectedDepartmentAllowance = await _context.Department
                    .Where(d => d.Id == selectedDepartmentId.Value)
                    .Select(d => d.Allowance)
                    .FirstOrDefaultAsync();

                RemainingAllowance = SelectedDepartmentAllowance ?? 0;
            }

            if (minAvgRating.HasValue)
            {
                MinAvgRating = (minAvgRating / 100.0); // Normalize to 0–1 scale

                query = query.Where(g =>
                    _context.GrantReview
                        .Where(r => r.GrantID == g.Id)
                        .GroupBy(r => r.GrantID)
                        .Select(grp => grp.Average(r => r.ReviewScore))
                        .FirstOrDefault() >= MinAvgRating);
            }

            Console.Write("\n\nGet in Post called\n\n");

            // Fetch the filtered grants
            var filteredGrants = await query.Where(g => g.Status == "Approved").ToListAsync();

            if (!filteredGrants.Any())
            {
                ErrorMessage = "No grants match the current filters.";
                Console.WriteLine($"\n\n{ErrorMessage}\n\n");
                return RedirectToPage(new { selectedDepartmentId, minAvgRating = minAvgRating * 100 });
            }

            // Populate approval ratings
                var ratings = await _context.GrantReview
                    .GroupBy(r => r.GrantID)
                    .Select(g => new { g.Key, AvgRating = g.Average(r => r.ReviewScore) })
                    .ToDictionaryAsync(g => g.Key, g => g.AvgRating / 54);

                foreach (var grant in filteredGrants)
                {
                    Console.Write($"grantId, {grant.Id}");
                    ApprovalRatings[grant.Id] = ratings.ContainsKey(grant.Id) ? ratings[grant.Id] : 0;
                }


            Console.WriteLine("\n\nApprovalRatings Key-Value Pairs:\n");
            foreach (var kvp in ApprovalRatings)
            {
                Console.WriteLine($"Grant ID: {kvp.Key}, Approval Rating: {kvp.Value}");
            }

            decimal totalDeduction = 0m;

            foreach (var grant in filteredGrants)
            {
                Console.WriteLine($"\nGrant ID: {grant.Id}, Title: {grant.Title}, Amount: {grant.Amount}, TotalAmount: {grant.TotalAmount}\n");

                if (!ApprovalRatings.ContainsKey(grant.Id)) continue;
                Console.Write("\n\nIt does?\n\n");

                var avgRating = ApprovalRatings[grant.Id] * 100; // Convert back to percentage

                foreach (var range in ranges)
                {
                    Console.WriteLine($"\range: {range}\n");
                    if (avgRating >= range.Min && avgRating <= range.Max)
                    {
                        Console.Write("avgRating >= range.Min && avgRating <= range.Max");
                        Console.Write($"{avgRating} >= {range.Min} && {avgRating} <= {range.Max} == {avgRating >= range.Min && avgRating <= range.Max}");
                        var deduction = grant.Amount * (decimal)(range.Percentage / 100);

                        if (RemainingAllowance < deduction)
                        {
                            ErrorMessage = "Insufficient funds in the department allowance.";
                            Console.Write($"\n{ErrorMessage}\n");
                            return RedirectToPage(new { selectedDepartmentId, minAvgRating = minAvgRating * 100 });
                        }

                        RemainingAllowance -= deduction;
                        grant.TotalAmount += deduction; // Update grant's allocated amount
                        totalDeduction += deduction;

                        break; // Stop after applying the first matching range
                    }
                }
            }

            if (totalDeduction > 0)
            {
                if (selectedDepartmentId.HasValue)
                {
                    var department = await _context.Department.FindAsync(selectedDepartmentId.Value);
                    if (department != null)
                    {
                        department.Allowance = RemainingAllowance;
                        Console.WriteLine($"Updated Department Allowance: {department.Allowance}");
                    }
                }

                await _context.SaveChangesAsync(); // Save changes to the database
                ErrorMessage = null;
            }
            else
            {
                ErrorMessage = "No grants were updated. Verify ranges and filters.";
            }

            return RedirectToPage(new { selectedDepartmentId, minAvgRating = minAvgRating * 100 });
        }


        private async Task GetApprovalRatingsAsync()
        {
            var ratings = await _context.GrantReview
                .GroupBy(r => r.GrantID)
                .Select(g => new { g.Key, AvgRating = g.Average(r => r.ReviewScore) })
                .ToDictionaryAsync(g => g.Key, g => g.AvgRating / 54);

            foreach (var grant in Grant)
            {
                ApprovalRatings[grant.Id] = ratings.ContainsKey(grant.Id) ? ratings[grant.Id] : 0;
            }
        }
    }

    public class RangeDto
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Percentage { get; set; }
    }
}
