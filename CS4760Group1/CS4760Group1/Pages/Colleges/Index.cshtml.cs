using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS4760Group1.Data;
using CS4760Group1.Models;

namespace CS4760Group1.Pages.Colleges
{
    public class IndexModel : PageModel
    {
        private readonly CS4760Group1.Data.CS4760Group1Context _context;

        public IndexModel(CS4760Group1.Data.CS4760Group1Context context)
        {
            _context = context;
        }

        public IList<College> College { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var colleges = from c in _context.College
                         select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                colleges = colleges.Where(c => c.Dean != null && c.Dean.Contains(SearchString));
            }

            College = await colleges.ToListAsync();
        }

        //public async Task OnGetAsync()
        //{
        //    College = await _context.College.ToListAsync();
        //}
    }
}
