using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CS4760Group1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public IndexModel(ILogger<IndexModel> logger, CS4760Group1.Data.CS4760Group1Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Models.Grant> Grants { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public List<Deadline> Deadlines { get; set; }

        public async Task OnGetAsync()
        {
            var grants = from c in _context.Grant
                         select c;
            if (grants.Any())
            {
                Grants = await grants.ToListAsync();
            }

            // Initialize Deadlines, even if there are no items
            Deadlines = new List<Deadline>
                        {
                            new Deadline{
                                GrantType = "Travel Grant",
                                Date = new DateTime(2024, 10, 10)
                            },
                            new Deadline{
                                GrantType = "Spring Grant",
                                Date = new DateTime(2024, 10, 10)
                            },
                            new Deadline{
                                GrantType = "Summer Grant",
                                Date = new DateTime(2024, 10, 10)
                            }
                        };
        }

        // Fixes a problem with the colored tags
        public string GetStatusClass(string status)
        {
            return status.ToLower().Replace(" ", "-");
        }

        //public void OnGet()
        //{
        //    Grants = new List<Grant>
        //    { 
        //        new Grant
        //        {
        //            Title = "Grant for Education Initiative",
        //            Status = "Not Reviewed",
        //            Type= "RSPG",
        //            Duration = 12,
        //            Amount = 50000,
        //            StatusClass = "not-reviewed"
        //        },
        //        new Grant
        //        {
        //            Title = "Healthcare Research Grant",
        //            Status = "Under Review",
        //            Type= "OUR",
        //            Duration = 24,
        //            Amount = 150000,
        //            StatusClass = "under-review"
        //        },
        //        new Grant
        //        {
        //            Title = "Community Development Fund",
        //            Status = "Approved",
        //            Type= "ARCC",
        //            Duration = 6,
        //            Amount = 25000,
        //            StatusClass = "approved"
        //        },
        //        new Grant
        //        {
        //            Title = "Environmental Conservation Grant",
        //            Status = "Not Reviewed",
        //            Type = "RSPG",
        //            Duration = 18,
        //            Amount = 80000,
        //            StatusClass = "not-reviewed"
        //        },
        //        new Grant
        //        {
        //            Title = "Innovative Tech Grant",
        //            Status = "Approved",
        //            Type = "OUR",
        //            Duration = 36,
        //            Amount = 200000,
        //            StatusClass = "approved"
        //        },
        //        new Grant
        //        {
        //            Title = "Arts and Culture Development",
        //            Status = "Under Review",
        //            Type = "ARCC",
        //            Duration = 12,
        //            Amount = 50000,
        //            StatusClass = "under-review"
        //        },
        //        new Grant
        //        {
        //            Title = "Rural Healthcare Support",
        //            Status = "Approved",
        //            Type = "OUR",
        //            Duration = 9,
        //            Amount = 75000,
        //            StatusClass = "approved"
        //        },
        //        new Grant
        //        {
        //            Title = "Renewable Energy Innovation",
        //            Status = "Under Review",
        //            Type = "OUR",
        //            Duration = 30,
        //            Amount = 300000,
        //            StatusClass = "under-review"
        //        },
        //        new Grant
        //        {
        //            Title = "STEM Education Support",
        //            Status = "Not Reviewed",
        //            Type = "RSPG",
        //            Duration = 24,
        //            Amount = 100000,
        //            StatusClass = "not-reviewed"
        //        },
        //        new Grant
        //        {
        //            Title = "Urban Infrastructure Grant",
        //            Status = "Approved",
        //            Type = "ARCC",
        //            Duration = 48,
        //            Amount = 500000,
        //            StatusClass = "approved"
        //        }
        //    };


        //    Deadlines = new List<Deadline>
        //    {
        //        new Deadline{
        //            GrantType = "Travel Grant",
        //            Date = new DateTime(2024, 10, 10)
        //        },
        //        new Deadline{
        //            GrantType = "Spring Grant",
        //            Date = new DateTime(2024, 10, 10)
        //        },
        //        new Deadline{
        //            GrantType = "Summer Grant",
        //            Date = new DateTime(2024, 10, 10)
        //        }
        //    };

        //}

        //public class Grant
        //{
        //    public string Title { get; set; }
        //    public string Status { get; set; }
        //    public string Type { get; set; }    
        //    public int Duration { get; set; }
        //    public decimal Amount { get; set; }
        //    public string StatusClass { get; set; }
        //}

        public class Deadline
        {
            public string GrantType { get; set; }
            public DateTime Date { get; set; }
        }

    }
}
