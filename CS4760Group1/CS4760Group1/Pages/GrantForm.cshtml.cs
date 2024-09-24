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
            GrantForm = new GrantForm();
        }

        [BindProperty]
        public GrantForm GrantForm { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() { //Handle form submission
            if (!ModelState.IsValid) {
                //checks that the page is valid based on specified attributes
                //returns the page with the error messages if not valid
                return Page();
            }

            //Post as not reviewed
            GrantForm.GrantStatus = "Not Reviewed";

            _context.GrantForm.Add(GrantForm);
            await _context.SaveChangesAsync();

            return RedirectToPage("GrantForm"); //Redirect to index page after submission
        }
    }
}
