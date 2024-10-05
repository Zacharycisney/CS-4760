using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CS4760Group1.Pages
{
    public class GrantDetailsModel : PageModel
    {
        private readonly CS4760Group1Context _context;

        public GrantDetailsModel(CS4760Group1Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Grant? Grant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find the grant by the provided ID
            Grant = await _context.Grant.FirstOrDefaultAsync(m => m.Id == id);

            // If the grant is not found, return a "Not Found" response
            if (Grant == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
