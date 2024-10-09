using CS4760Group1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS4760Group1.Data;
using Microsoft.AspNetCore.Http;
using System.IO;


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
            GrantFile = new GrantFile();
        }

        [BindProperty]
        public Grant Grant { get; set; }

        [BindProperty]
        public IFormFile File { get; set; }

        [BindProperty]
        public GrantFile GrantFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // First, save the grant details
            Grant.Status = "Not Reviewed";

            _context.Grant.Add(Grant);
            await _context.SaveChangesAsync(); // Save to generate GrantId

            // If a file is uploaded
            if (File != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }  

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + File.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }

                // Save the file details in the GrantFile table
                GrantFile = new GrantFile
                {
                    GrantId = Grant.Id,  // Link the file to the Grant
                    FileName = File.FileName,
                    FilePath = "/uploads/" + uniqueFileName
                };

                _context.GrantFile.Add(GrantFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
// maybe go back to isabels version in github