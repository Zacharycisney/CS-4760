using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS4760Group1.Pages
{
    public class GrantDetailsModel : PageModel
    {
        public GrantInfo Grant { get; set; }

        public void OnGet(int id)
        {
            // Sample data representing detailed grant information
            var grants = new List<GrantInfo>
            {
                new GrantInfo { Id = 1, Title = "Grant for Education Initiative", Status = "Not Reviewed", Type = "RSPG", Duration = 12, Amount = 50000, Description = "A grant for enhancing educational initiatives", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2025, 1, 1) },
                new GrantInfo { Id = 2, Title = "Healthcare Research Grant", Status = "Under Review", Type = "OUR", Duration = 24, Amount = 150000, Description = "Research grant focused on healthcare improvements", StartDate = new DateTime(2023, 6, 1), EndDate = new DateTime(2025, 6, 1) },
                new GrantInfo { Id = 3, Title = "Community Development Fund", Status = "Approved", Type = "ARCC", Duration = 6, Amount = 25000, Description = "Funding for community development projects", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 9, 1) }
            };

            // Fetch the grant by its id
            Grant = grants.FirstOrDefault(g => g.Id == id);
        }

        public class GrantInfo
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }
            public int Duration { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
