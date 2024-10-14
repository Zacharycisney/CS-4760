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
using static CS4760Group1.Pages.IndexModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;


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
        public List<SelectListItem> UserList { get; set; }

        [BindProperty]
        public IFormFile GrantUpload { get; set; }


        public List<GrantType> AppliedGrant { get; set; }
        public IList<Department> Department { get; set; } = new List<Department>();
        public IList<College> College { get; set; } = new List<College>();




        public async Task OnGet()
        {
            PopulateUserList();


            Department = await _context.Department.ToListAsync();
            College = await _context.College.ToListAsync();

            AppliedGrant = new List<GrantType> { 
                new GrantType{ 
                    applyingFor = "Research Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Creative Works Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Travel Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Excellence Award",
                    grantSeason = "Spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Collaborative Award",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Innovative Teaching Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Adjunct Faculty Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway New Faculty Grant",
                    grantSeason = "Spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Faculty Vitality Grant",
                    grantSeason = "Fall Spring"
                },
                new GrantType{
                    applyingFor = "Community Engaged Learning Grant",
                    grantSeason = "Fall Spring"
                }
            };

        }

        /// <summary>
        /// Auto populates the PI 
        /// </summary>
        private void PopulateUserList()
        {
            UserList = _context.Users
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync() { //Handle form submission

            // Repopulate UserList when the form is submitted
            PopulateUserList();

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

        public class GrantType
        {
            public string applyingFor { get; set; }
            public string grantSeason { get; set; }
        }
    }
}
