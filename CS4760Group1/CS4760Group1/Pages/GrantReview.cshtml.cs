using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CS4760Group1.Pages.IndexModel;

namespace CS4760Group1.Pages
{
    public class GrantReviewModel : PageModel
    {
        public List<Grant> Grants { get; set; }
        public string SearchString { get; set; }

        public void OnGet()
        {
            Grants = new List<Grant>
            {
                new Grant { Id = 1,Type= "RSPG",  Title = "Grant for Education Initiative", PI = "User" },
                new Grant { Id = 2, Type= "OUR", Title = "Healthcare Research Grant",  PI = "User" },
                new Grant { Id = 3, Type= "ARCC", Title = "Community Development Fund", PI = "User" }
            };
        }

        public class Grant
        {
            public int Id { get; set; }
            public string Type  { get; set; }
            public string Title { get; set; }
            public string PI { get; set; }
        }
    }
    
}
