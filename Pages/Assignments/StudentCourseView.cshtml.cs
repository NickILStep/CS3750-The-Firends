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
    [Authorize(Policy = "MustBeStudent")]
    public class StudentCourseViewModel : PageModel
    {
        private readonly Assignment1v3Context _context;

        public StudentCourseViewModel(Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Assignment> Assignments { get; set; } = new List<Assignment>();
        public Course SelectedCourse { get; set; }
        public List<Assignment> Assignment { get; private set; }
        public Dictionary<int, int?> HighestGrades { get; set; } = new Dictionary<int, int?>();

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            //Debuging stuff
            System.Diagnostics.Debug.WriteLine(courseId);
            System.Diagnostics.Debug.WriteLine(_context.Assignment);
            if (_context.Assignment != null)
            {
                Assignment = await _context.Assignment.ToListAsync();
                System.Diagnostics.Debug.WriteLine(Assignment);
            }

            int StudentID = int.Parse(this.User.Claims.ElementAt(3).Value);


            SelectedCourse = await _context.Course
                .Where(c => c.Id == courseId)
                .FirstOrDefaultAsync();
            System.Diagnostics.Debug.WriteLine(SelectedCourse);

            if (SelectedCourse == null)
            {
                // Course not found, handle accordingly (e.g., return a not found page).
                return NotFound();
            }

            Assignment = await _context.Assignment
                .Where(a => a.course == SelectedCourse.Id) // Assuming 'course' is a string in the Assignment model
                .ToListAsync();
            System.Diagnostics.Debug.WriteLine(Assignment);

            foreach (var assignment in Assignment)
            {
                var highestGrade = await _context.Submission
                    .Where(s => s.AssignmentID == assignment.ID && s.UserID == StudentID)
                    .MaxAsync(s => (int?)s.PointsEarned);

                HighestGrades[assignment.ID] = highestGrade;
            }

            return Page();
        }
    }
}
