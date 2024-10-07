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

        [BindProperty]
        public IFormFile GrantUpload { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() { //Handle form submission
           
            //Submit grant to Grant table
            Grant.Status = "Not Reviewed";

            _context.Grant.Add(Grant);
            await _context.SaveChangesAsync();

            //Submit file to GrantFile table

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "grant_files", GrantUpload.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await GrantUpload.CopyToAsync(stream);
            }

            var grantFile = new GrantFile
            {
                GrantID = Grant.Id,
                FileName = GrantUpload.FileName,
                Location = filePath
            };

            _context.GrantFile.Add(grantFile);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); //Redirect to index page after submission
        }
    }
}
