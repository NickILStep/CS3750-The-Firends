using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;

namespace Assignment1v3.Pages.Home
{
    public class DropCourseModel : PageModel
    {
        private readonly Assignment1v3Context _context;

        public DropCourseModel(Assignment1v3Context context)
        {
            _context = context;
        }

        [BindProperty]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            Course = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var courseToDrop = await _context.Course.FindAsync(CourseId);

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
