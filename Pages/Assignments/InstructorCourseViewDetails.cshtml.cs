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

namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeInstructor")]
    public class InstructorCourseViewDetailsModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public InstructorCourseViewDetailsModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

      public Assignment Assignment { get; set; } = default!;
        public Course Course { get; set; } // Change to a single Course object

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assignment == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }
            else
            {
                Assignment = assignment;

                // Assuming there is a property in Assignment model that represents course name
                var courseName = assignment.course;

                // Ensure that the courseName is not null or empty
                if (!string.IsNullOrEmpty(courseName))
                {
                    Course = await _context.Course.FirstOrDefaultAsync(c => c.CourseName == courseName);
                }
            }
            return Page();
        }
    }
}
