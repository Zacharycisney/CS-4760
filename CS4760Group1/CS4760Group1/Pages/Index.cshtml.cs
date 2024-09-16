using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS4760Group1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Grant> Grants { get; set; }

        public void OnGet()
        {
            Grants = new List<Grant>
        {
            new Grant
            {
                Title = "Grant for Education Initiative",
                Status = "Not Reviewed",
                Type= "RSPG",
                Duration = 12,
                Amount = 50000,
                StatusClass = "not-reviewed"
            },
            new Grant
            {
                Title = "Healthcare Research Grant",
                Status = "Under Review",
                Type= "OUR",
                Duration = 24,
                Amount = 150000,
                StatusClass = "under-review"
            },
            new Grant
            {
                Title = "Community Development Fund",
                Status = "Approved",
                Type= "ARCC",
                Duration = 6,
                Amount = 25000,
                StatusClass = "approved"
            }
        };
        }

        public class Grant
        {
            public string Title { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }    
            public int Duration { get; set; }
            public decimal Amount { get; set; }
            public string StatusClass { get; set; }
        }
    }
}
