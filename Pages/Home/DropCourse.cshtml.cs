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
using Microsoft.AspNetCore.Authentication;

namespace Assignment1v3.Pages.Home
{
    [Authorize(Policy = "MustBeStudent")]
    public class DropCourseModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public DropCourseModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var courseToDrop = await _context.Course.FindAsync(id);

            if (courseToDrop != null)
            {
                // Remove the course from the database
                _context.Course.Remove(courseToDrop);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}

