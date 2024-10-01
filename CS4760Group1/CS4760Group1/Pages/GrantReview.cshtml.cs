using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static CS4760Group1.Pages.IndexModel;

namespace CS4760Group1.Pages
{
    public class GrantReviewModel(CS4760Group1.Data.CS4760Group1Context context) : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context = context;

        public IList<Models.Grant> Grants { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var grants = from c in _context.Grant
                           select c;
            if (grants.Any())
            {
                Grants = await grants.ToListAsync();
            }
        }

        //public void OnGet()
        //{
        //    Grants = new List<Grant>
        //    {
        //        new Grant { Id = 1,Type= "RSPG",  Title = "Grant for Education Initiative", PI = "User" },
        //        new Grant { Id = 2, Type= "OUR", Title = "Healthcare Research Grant",  PI = "User" },
        //        new Grant { Id = 3, Type= "ARCC", Title = "Community Development Fund", PI = "User" }
        //    };
        //}

        //public class Grant
        //{
        //    public int Id { get; set; }
        //    public string Type  { get; set; }
        //    public string Title { get; set; }
        //    public string PI { get; set; }
        //}
    }
    
}
