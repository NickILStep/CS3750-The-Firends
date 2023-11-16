using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeStudent")]
    public class StudentCourseViewDetailsModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public StudentCourseViewDetailsModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public Assignment Assignment { get; set; } = default!;
        public Course Course { get; set; } // Change to a single Course object

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);
            Assignment = assignment;
            var courseId = assignment.course;

            if (courseId != 0) // Assuming 0 is not a valid course ID
            {
                Course = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
            }

            return Page();
        }
    }
}
