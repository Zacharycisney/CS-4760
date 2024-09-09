using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS4760Group1.Data;
using CS4760Group1.Models;

namespace CS4760Group1.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public IndexModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var departmnets = from d in _context.Department
                           select d;
            if (!string.IsNullOrEmpty(SearchString))
            {
                departmnets = departmnets.Where(d => d.Name != null && d.Name.Contains(SearchString));
            }

            Department = await departmnets.ToListAsync();
        }

        //public async Task OnGetAsync()
        //{
        //    Department = await _context.Department.ToListAsync();
        //}
    }
}
