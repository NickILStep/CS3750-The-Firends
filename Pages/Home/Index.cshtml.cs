using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment1v3.Pages.Home
{
    //[Authorize(Policy = "MustBeStudent")] // Add this attribute to restrict access
    public class IndexModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public IndexModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }
        }
    }
}
