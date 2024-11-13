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
            GrantDraft = new GrantDraft();
        }

        public SelectList DraftList { get; set; }

        [BindProperty]
        public Grant Grant { get; set; }
        [BindProperty]
        public GrantDraft GrantDraft { get; set; }
        public List<SelectListItem> UserList { get; set; }

        [BindProperty]
        public IFormFile GrantUpload { get; set; }
        [BindProperty]
        public IFormFile GrantUpload2 { get; set; }
        [BindProperty]
        public IFormFile GrantUpload3 { get; set; }

        [BindProperty]
        public IFormFile ApprovalUpload { get; set; }


        public List<GrantType> AppliedGrant { get; set; }
        public IList<Department> Department { get; set; } = new List<Department>();
        public IList<College> College { get; set; } = new List<College>();




        public async Task OnGet()
        {
            PopulateUserList();
            //DraftList = await PopulateDraftList();


            Department = await _context.Department.ToListAsync();
            College = await _context.College.ToListAsync();

            AppliedGrant = new List<GrantType> { 
                new GrantType{ 
                    applyingFor = "Research Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Creative Works Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Travel Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Excellence Award",
                    grantSeason = "spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Collaborative Award",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Innovative Teaching Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Adjunct Faculty Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway New Faculty Grant",
                    grantSeason = "spring"
                },
                new GrantType{
                    applyingFor = "Hemmingway Faculty Vitality Grant",
                    grantSeason = "fall,spring"
                },
                new GrantType{
                    applyingFor = "Community Engaged Learning Grant",
                    grantSeason = "fall,spring"
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

        private async Task<SelectList> PopulateDraftList()
        {
            var drafts = await _context.GrantDraft
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Title
                })
                .ToListAsync();

            // Ensure drafts is not null and has at least one item
            if (drafts == null || !drafts.Any())
            {
                return new SelectList(new List<SelectListItem>());
            }

            return new SelectList(drafts, "Value", "Text");
        }



        public async Task<IActionResult> OnPostAsync() { //Handle form submission

            // Repopulate UserList when the form is submitted
            PopulateUserList();
            //DraftList = await PopulateDraftList();


            var submitType = Request.Form["action"]; //Get the value of if its being submitted as a draft or completed document

            if (submitType == "submitForm")
            {

                if ((GrantUpload == null || GrantUpload.Length == 0) &&
                    (GrantUpload2 == null || GrantUpload2.Length == 0) &&
                    (GrantUpload3 == null || GrantUpload3.Length == 0))
                {
                    ModelState.AddModelError(string.Empty, "Please upload a file.");
                    return Page();
                }

                Grant.Title = GrantDraft.Title;
                Grant.PI = GrantDraft.PI;
                Grant.Description = GrantDraft.Description;
                Grant.CollegeID = GrantDraft.CollegeID;
                Grant.DepartmentID = GrantDraft.DepartmentID;
                Grant.Type = GrantDraft.Type;
                Grant.Season = GrantDraft.Season;
                Grant.SubType = GrantDraft.SubType;
                Grant.Amount = GrantDraft.Amount;
                Grant.AmountFromOther = GrantDraft.AmountFromOther;
                Grant.TotalAmount = GrantDraft.TotalAmount;
                Grant.ProcMethod = GrantDraft.ProcMethod;
                Grant.Timeline = GrantDraft.Timeline;
                Grant.Index = GrantDraft.Index;
                Grant.SubjectNeeded = GrantDraft.SubjectNeeded;


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

                string approvalUploadsFolder = Path.Combine(_environment.WebRootPath, "approval_files");
                if (!Directory.Exists(approvalUploadsFolder))
                {
                    Directory.CreateDirectory(approvalUploadsFolder); // Create directory if it doesn't exist
                }



                if (GrantUpload != null && GrantUpload.Length > 0)
                {
                    string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + GrantUpload.FileName;
                    string filePath1 = Path.Combine(uploadsFolder, uniqueFileName1);

                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        await GrantUpload.CopyToAsync(stream);
                    }

                    // Add GrantFile record for the first file
                    var grantFile1 = new GrantFile
                    {
                        GrantID = Grant.Id,
                        FileName = GrantUpload.FileName,
                        FilePath = "/grant_files/" + uniqueFileName1,
                        Grant = Grant
                    };

                    _context.GrantFile.Add(grantFile1);
                }

                // Handle GrantUpload2 (File 2)
                if (GrantUpload2 != null && GrantUpload2.Length > 0)
                {
                    string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + GrantUpload2.FileName;
                    string filePath2 = Path.Combine(uploadsFolder, uniqueFileName2);

                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        await GrantUpload2.CopyToAsync(stream);
                    }

                    // Add GrantFile record for the second file
                    var grantFile2 = new GrantFile
                    {
                        GrantID = Grant.Id,
                        FileName = GrantUpload2.FileName,
                        FilePath = "/grant_files/" + uniqueFileName2,
                        Grant = Grant
                    };

                    _context.GrantFile.Add(grantFile2);
                }

                // Handle GrantUpload3 (File 3)
                if (GrantUpload3 != null && GrantUpload3.Length > 0)
                {
                    string uniqueFileName3 = Guid.NewGuid().ToString() + "_" + GrantUpload3.FileName;
                    string filePath3 = Path.Combine(uploadsFolder, uniqueFileName3);

                    using (var stream = new FileStream(filePath3, FileMode.Create))
                    {
                        await GrantUpload3.CopyToAsync(stream);
                    }

                    // Add GrantFile record for the third file
                    var grantFile3 = new GrantFile
                    {
                        GrantID = Grant.Id,
                        FileName = GrantUpload3.FileName,
                        FilePath = "/grant_files/" + uniqueFileName3,
                        Grant = Grant
                    };

                    _context.GrantFile.Add(grantFile3);
                }

                if (ApprovalUpload != null && ApprovalUpload.Length > 0)
                {
                    string approvalFileName = Guid.NewGuid().ToString() + "_" + ApprovalUpload.FileName;
                    string approvalFilePath = Path.Combine(approvalUploadsFolder, approvalFileName);

                    using (var stream = new FileStream(approvalFilePath, FileMode.Create))
                    {
                        await ApprovalUpload.CopyToAsync(stream);
                    }

                    // Add GrantFile record for the third file
                    var approvalFile = new GrantFile
                    {
                        GrantID = Grant.Id,
                        FileName = ApprovalUpload.FileName,
                        FilePath = "/approval_files/" + approvalFileName,
                        Grant = Grant
                    };

                    _context.GrantFile.Add(approvalFile);
                }
            }
            else
            {
                _context.GrantDraft.Add(GrantDraft);
            }

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
