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

        [BindProperty]
        public bool IsDraft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            PopulateUserList();
            Department = await _context.Department.ToListAsync();
            College = await _context.College.ToListAsync();

            AppliedGrant = new List<GrantType>
            {
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

            if (id.HasValue)  // Edit mode: load the existing grant by ID
            {
                Grant = await _context.Grant.FirstOrDefaultAsync(m => m.Id == id.Value);
                if (Grant == null) return NotFound();


                if (Grant == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Grant = new Grant();  // New grant if no ID is provided
            }

            return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            // Repopulate lists and dropdowns when the form is submitted
            PopulateUserList();
            Department = await _context.Department.ToListAsync();
            College = await _context.College.ToListAsync();

            

            if (IsDraft) // If it's a draft submission
            {
                ModelState.Clear();

                // Create a new GrantDraft model and map the properties from the Grant model
                GrantDraft grantDraft = new GrantDraft
                {
                    Title = Grant.Title,
                    PI = Grant.PI,
                    Description = Grant.Description,
                    Type = Grant.Type,
                    Amount = Grant.Amount,
                    Status = "Draft",  // Draft status
                    SubType = Grant.SubType,
                    Season = Grant.Season,
                    AmountFromOther = Grant.AmountFromOther,
                    TotalAmount = Grant.TotalAmount,
                    CollegeID = Grant.CollegeID,
                    DepartmentID = Grant.DepartmentID,
                    ProcMethod = Grant.ProcMethod,
                    Timeline = Grant.Timeline,
                    Index = Grant.Index,
                    SubjectNeeded = Grant.SubjectNeeded
                };

                // Add the draft to the database
                _context.GrantDraft.Add(grantDraft);
            }
            else
            {

                if (Grant.Id > 0) // Check if we're in edit mode for already submitted grants (NOT drafts)
                {
                    // Edit mode: load existing grant and update its properties
                    var existingGrant = await _context.Grant.FirstOrDefaultAsync(g => g.Id == Grant.Id);
                    if (existingGrant == null)
                    {
                        return NotFound("The grant was not found for editing.");
                    }

                    // Update existing grant properties
                    existingGrant.Title = Grant.Title;
                    existingGrant.PI = Grant.PI;
                    existingGrant.Description = Grant.Description;
                    existingGrant.Type = Grant.Type;
                    existingGrant.Amount = Grant.Amount;
                    existingGrant.SubType = Grant.SubType;
                    existingGrant.Season = Grant.Season;
                    existingGrant.AmountFromOther = Grant.AmountFromOther;
                    existingGrant.TotalAmount = Grant.TotalAmount;
                    existingGrant.CollegeID = Grant.CollegeID;
                    existingGrant.DepartmentID = Grant.DepartmentID;
                    existingGrant.ProcMethod = Grant.ProcMethod;
                    existingGrant.Timeline = Grant.Timeline;
                    existingGrant.Index = Grant.Index;
                    existingGrant.SubjectNeeded = Grant.SubjectNeeded;

                    // Attach and mark as modified
                    _context.Attach(existingGrant).State = EntityState.Modified;
                }
                else
                {
                    // Create mode: add a new grant with default status
                    Grant.Status = "Not Reviewed";
                    _context.Grant.Add(Grant);
                }

                // Save each uploaded file
                await SaveFiles(Grant);
            }

            // Save changes to database to assign Grant.Id if it's a new grant
            await _context.SaveChangesAsync();

            


            return RedirectToPage("Index");
        }

        private async Task SaveFiles(Grant grant)
        {
            // Define folders for saving files
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "grant_files");
            string approvalUploadsFolder = Path.Combine(_environment.WebRootPath, "approval_files");

            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            if (!Directory.Exists(approvalUploadsFolder)) Directory.CreateDirectory(approvalUploadsFolder);

            // Save files if any were uploaded
            if (GrantUpload != null && GrantUpload.Length > 0)
            {
                await SaveFile(GrantUpload, uploadsFolder, "grant_files", grant.Id);
            }
            if (GrantUpload2 != null && GrantUpload2.Length > 0)
            {
                await SaveFile(GrantUpload2, uploadsFolder, "grant_files", grant.Id);
            }
            if (GrantUpload3 != null && GrantUpload3.Length > 0)
            {
                await SaveFile(GrantUpload3, uploadsFolder, "grant_files", grant.Id);
            }
            if (ApprovalUpload != null && ApprovalUpload.Length > 0)
            {
                await SaveFile(ApprovalUpload, approvalUploadsFolder, "approval_files", grant.Id);
            }
        }

        private async Task SaveFile(IFormFile file, string folderPath, string folderType, int grantId)
        {
            // Generate a unique file name and file path
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(folderPath, uniqueFileName);

            // Save the file to the specified folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create a GrantFile entry and associate it with the grant
            var grantFile = new GrantFile
            {
                GrantID = grantId,
                FileName = file.FileName,
                FilePath = $"/{folderType}/{uniqueFileName}", // Store relative path for accessibility
                Grant = Grant
            };

            _context.GrantFile.Add(grantFile);
        }

        public class GrantType
        {
            public string applyingFor { get; set; }
            public string grantSeason { get; set; }
        }
    }
}
