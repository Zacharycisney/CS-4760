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
        [BindProperty]
        public IFormFile GrantUpload2 { get; set; }
        [BindProperty]
        public IFormFile GrantUpload3 { get; set; }

        [BindProperty]
        public IFormFile ApprovalUpload { get; set; }


        public List<GrantType> AppliedGrant { get; set; }
        public IList<Department> Department { get; set; } = new List<Department>();
        public IList<College> College { get; set; } = new List<College>();

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


        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    PopulateUserList();
        //    Department = await _context.Department.ToListAsync();
        //    College = await _context.College.ToListAsync();

        //    AppliedGrant = new List<GrantType>
        //    {
        //        new GrantType{
        //            applyingFor = "Research Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Creative Works Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Travel Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Excellence Award",
        //            grantSeason = "spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Collaborative Award",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Innovative Teaching Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Adjunct Faculty Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway New Faculty Grant",
        //            grantSeason = "spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Faculty Vitality Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Community Engaged Learning Grant",
        //            grantSeason = "fall,spring"
        //        }
        //    };

        //    if (id.HasValue)  // Edit mode: load the existing grant by ID
        //    {
        //        Grant = await _context.Grant.FirstOrDefaultAsync(m => m.Id == id.Value);

        //        if (Grant == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    else
        //    {
        //        Grant = new Grant();  // New grant if no ID is provided
        //    }

        //    return Page();
        //}


        //public async Task OnGet()
        //{
        //    PopulateUserList();


        //    Department = await _context.Department.ToListAsync();
        //    College = await _context.College.ToListAsync();

        //    AppliedGrant = new List<GrantType> { 
        //        new GrantType{ 
        //            applyingFor = "Research Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Creative Works Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Travel Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Excellence Award",
        //            grantSeason = "spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Collaborative Award",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Innovative Teaching Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Adjunct Faculty Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway New Faculty Grant",
        //            grantSeason = "spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Hemmingway Faculty Vitality Grant",
        //            grantSeason = "fall,spring"
        //        },
        //        new GrantType{
        //            applyingFor = "Community Engaged Learning Grant",
        //            grantSeason = "fall,spring"
        //        }
        //    };

        //}

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

            if (Grant.Id > 0) // Check if we're in edit mode
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

            // Save changes to database to assign Grant.Id if it's a new grant
            await _context.SaveChangesAsync();

            // Save each uploaded file
            await SaveFiles(Grant);

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

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    // Repopulate UserList when the form is submitted
        //    PopulateUserList();

        //    if ((GrantUpload == null || GrantUpload.Length == 0) &&
        //        (GrantUpload2 == null || GrantUpload2.Length == 0) &&
        //        (GrantUpload3 == null || GrantUpload3.Length == 0) &&
        //        (ApprovalUpload == null || ApprovalUpload.Length == 0))
        //    {
        //        ModelState.AddModelError(string.Empty, "Please upload at least one file.");
        //        return Page();
        //    }

        //    // Check if we are in edit mode or create mode
        //    if (Grant.Id > 0)
        //    {
        //        // Edit mode: Attach and mark as modified
        //        existingGrant.Title = Grant.Title;
        //        existingGrant.PI = Grant.PI;
        //        existingGrant.Description = Grant.Description;
        //        existingGrant.Type = Grant.Type;
        //        existingGrant.Amount = Grant.Amount;
        //        existingGrant.Status = Grant.Status;
        //        existingGrant.SubType = Grant.SubType;
        //        existingGrant.Season = Grant.Season;
        //        existingGrant.AmountFromOther = Grant.AmountFromOther;
        //        existingGrant.TotalAmount = Grant.TotalAmount;
        //        existingGrant.CollegeID = Grant.CollegeID;
        //        existingGrant.DepartmentID = Grant.DepartmentID;
        //        existingGrant.ProcMethod = Grant.ProcMethod;
        //        existingGrant.Timeline = Grant.Timeline;
        //        existingGrant.Index = Grant.Index;
        //        existingGrant.SubjectNeeded = Grant.SubjectNeeded;


        //        _context.Attach(Grant).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        // Create mode: Add the new grant with default status
        //        Grant.Status = "Not Reviewed";
        //        _context.Grant.Add(Grant);
        //    }

        //    await _context.SaveChangesAsync(); // Save to generate Grant.Id if new

        //    // Define folders for saving files
        //    string uploadsFolder = Path.Combine(_environment.WebRootPath, "grant_files");
        //    string approvalUploadsFolder = Path.Combine(_environment.WebRootPath, "approval_files");

        //    // Ensure directories exist
        //    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
        //    if (!Directory.Exists(approvalUploadsFolder)) Directory.CreateDirectory(approvalUploadsFolder);

        //    // Save each uploaded file and associate it with the grant
        //    if (GrantUpload != null && GrantUpload.Length > 0)
        //    {
        //        await SaveFile(GrantUpload, uploadsFolder, "grant_files", Grant.Id);
        //    }

        //    if (GrantUpload2 != null && GrantUpload2.Length > 0)
        //    {
        //        await SaveFile(GrantUpload2, uploadsFolder, "grant_files", Grant.Id);
        //    }

        //    if (GrantUpload3 != null && GrantUpload3.Length > 0)
        //    {
        //        await SaveFile(GrantUpload3, uploadsFolder, "grant_files", Grant.Id);
        //    }

        //    if (ApprovalUpload != null && ApprovalUpload.Length > 0)
        //    {
        //        await SaveFile(ApprovalUpload, approvalUploadsFolder, "approval_files", Grant.Id);
        //    }

        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("Index");
        //}

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

        //public async Task<IActionResult> OnPostAsync() { //Handle form submission

        //    // Repopulate UserList when the form is submitted
        //    PopulateUserList();

        //    if ((GrantUpload == null || GrantUpload.Length == 0) &&
        //        (GrantUpload2 == null || GrantUpload2.Length == 0) &&
        //        (GrantUpload3 == null || GrantUpload3.Length == 0))
        //    {
        //        ModelState.AddModelError(string.Empty, "Please upload a file.");
        //        return Page();
        //    }

        //    //Submit grant to Grant table
        //    Grant.Status = "Not Reviewed";

        //    _context.Grant.Add(Grant);
        //    //await _context.SaveChangesAsync();

        //    //Submit file to GrantFile table

        //    //path to save file
        //    string uploadsFolder = Path.Combine(_environment.WebRootPath, "grant_files");
        //    if (!Directory.Exists(uploadsFolder))
        //    {
        //        Directory.CreateDirectory(uploadsFolder); // Create directory if it doesn't exist
        //    }

        //    string approvalUploadsFolder = Path.Combine(_environment.WebRootPath, "approval_files");
        //    if (!Directory.Exists(approvalUploadsFolder))
        //    {
        //        Directory.CreateDirectory(approvalUploadsFolder); // Create directory if it doesn't exist
        //    }

        //    if (GrantUpload != null && GrantUpload.Length > 0)
        //    {
        //        string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + GrantUpload.FileName;
        //        string filePath1 = Path.Combine(uploadsFolder, uniqueFileName1);

        //        using (var stream = new FileStream(filePath1, FileMode.Create))
        //        {
        //            await GrantUpload.CopyToAsync(stream);
        //        }

        //        // Add GrantFile record for the first file
        //        var grantFile1 = new GrantFile
        //        {
        //            GrantID = Grant.Id,
        //            FileName = GrantUpload.FileName,
        //            FilePath = "/grant_files/" + uniqueFileName1,
        //            Grant = Grant
        //        };

        //        _context.GrantFile.Add(grantFile1);
        //    }

        //    // Handle GrantUpload2 (File 2)
        //    if (GrantUpload2 != null && GrantUpload2.Length > 0)
        //    {
        //        string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + GrantUpload2.FileName;
        //        string filePath2 = Path.Combine(uploadsFolder, uniqueFileName2);

        //        using (var stream = new FileStream(filePath2, FileMode.Create))
        //        {
        //            await GrantUpload2.CopyToAsync(stream);
        //        }

        //        // Add GrantFile record for the second file
        //        var grantFile2 = new GrantFile
        //        {
        //            GrantID = Grant.Id,
        //            FileName = GrantUpload2.FileName,
        //            FilePath = "/grant_files/" + uniqueFileName2,
        //            Grant = Grant
        //        };

        //        _context.GrantFile.Add(grantFile2);
        //    }

        //    // Handle GrantUpload3 (File 3)
        //    if (GrantUpload3 != null && GrantUpload3.Length > 0)
        //    {
        //        string uniqueFileName3 = Guid.NewGuid().ToString() + "_" + GrantUpload3.FileName;
        //        string filePath3 = Path.Combine(uploadsFolder, uniqueFileName3);

        //        using (var stream = new FileStream(filePath3, FileMode.Create))
        //        {
        //            await GrantUpload3.CopyToAsync(stream);
        //        }

        //        // Add GrantFile record for the third file
        //        var grantFile3 = new GrantFile
        //        {
        //            GrantID = Grant.Id,
        //            FileName = GrantUpload3.FileName,
        //            FilePath = "/grant_files/" + uniqueFileName3,
        //            Grant = Grant
        //        };

        //        _context.GrantFile.Add(grantFile3);
        //    }

        //    if (ApprovalUpload != null && ApprovalUpload.Length > 0)
        //    {
        //        string approvalFileName = Guid.NewGuid().ToString() + "_" + ApprovalUpload.FileName;
        //        string approvalFilePath = Path.Combine(approvalUploadsFolder, approvalFileName);

        //        using (var stream = new FileStream(approvalFilePath, FileMode.Create))
        //        {
        //            await ApprovalUpload.CopyToAsync(stream);
        //        }

        //        // Add GrantFile record for the third file
        //        var approvalFile = new GrantFile
        //        {
        //            GrantID = Grant.Id,
        //            FileName = ApprovalUpload.FileName,
        //            FilePath = "/approval_files/" + approvalFileName,
        //            Grant = Grant
        //        };

        //        _context.GrantFile.Add(approvalFile);
        //    }

        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("Index"); //Redirect to index page after submission
        //}

        public class GrantType
        {
            public string applyingFor { get; set; }
            public string grantSeason { get; set; }
        }
    }
}
