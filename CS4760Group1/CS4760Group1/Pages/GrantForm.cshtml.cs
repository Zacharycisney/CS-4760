using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;
using Microsoft.Extensions.Hosting;


namespace CS4760Group1.Pages
{
    public class GrantFormModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;
        private readonly IWebHostEnvironment _environment;
        public GrantFormModel(CS4760Group1.Data.CS4760Group1Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

            if (GrantUpload == null || GrantUpload.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please upload a file.");
                return Page();
            }

            //Submit grant to Grant table
            Grant.Status = "Not Reviewed";

            _context.Grant.Add(Grant);
            //await _context.SaveChangesAsync();

            //Submit file to GrantFile table

            //path to save file
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "grant_files");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); // Create directory if it doesn't exist
            }


            string uniqueFileName = Guid.NewGuid().ToString() + "_" + GrantUpload.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await GrantUpload.CopyToAsync(stream);
            }

            //object to submit to table
            var grantFile = new GrantFile
            {
                GrantID = Grant.Id,
                FileName = GrantUpload.FileName,
                FilePath = "/grant_files/" + uniqueFileName,  // Save relative path for easier access in the app
                Grant = Grant
            };

            _context.GrantFile.Add(grantFile);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); //Redirect to index page after submission
        }
    }
}
