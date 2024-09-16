using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS4760Group1.Pages
{
    public class GrantFormModel : PageModel
    {
        [BindProperty]
        public GrantForm GrantForm { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost() { //Handle form submission
            if (!ModelState.IsValid) {
                //checks that the page is valid based on specified attributes
                //returns the page with the error messages if not valid
                return Page();
            }

            //[Save to DB here]

           return RedirectToPage("GrantForm"); //Redirect to index page after submission
        }
    }
}
