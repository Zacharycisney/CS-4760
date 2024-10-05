using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static CS4760Group1.Pages.IndexModel;

namespace CS4760Group1.Pages
{
    public class GrantReviewModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        // Ensure the context is injected through the constructor
        public GrantReviewModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));  // Add null check for safety
        }

        public IList<Grant> Grants { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? GrantType { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(GrantType))
            {
                var grants = from c in _context.Grant
                             where c.Type == GrantType
                             select c;

                Grants = await grants.ToListAsync();
            }
            else
            {
                // If GrantType is null or empty, Grants remains empty
                Grants = new List<Grant>();
            }
        }
    }
}
