using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;


namespace CS4760Group1.Pages
{
    public class GrantFormModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public GrantFormModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
            Grant = new Grant();
        }

        [BindProperty]
        public Grant Grant { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() { //Handle form submission
           
            //Post as not reviewed
            Grant.Status = "Not Reviewed";

            _context.Grant.Add(Grant);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); //Redirect to index page after submission
        }
    }
}
