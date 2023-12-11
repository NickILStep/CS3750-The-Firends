using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

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
        public int? HighestGrade { get; set; }
        public double? HighestGradePer { get; set; }
        public double? AverageGradePer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);
            Assignment = assignment;
            var courseId = assignment.course;

            if (courseId != 0) // Assuming 0 is not a valid course ID
            {
                Course = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
            }

            int CurrentStudentID = int.Parse(this.User.Claims.ElementAt(3).Value);

            //get Highest submission score for current user for this assignment
            HighestGrade = await _context.Submission
            .Where(s => s.AssignmentID == assignment.ID && s.UserID == CurrentStudentID)
            .MaxAsync(s => (int?)s.PointsEarned);

            if (HighestGrade.HasValue)
            {
                HighestGradePer = ((double)HighestGrade / (double)assignment.maxPoints) * 100;//getting percent
            }

            //Get List of each submission that's been graded
            var highestGradesForEachUser = _context.Submission
             .Where(s => s.Graded == true && s.UserID != CurrentStudentID && s.AssignmentID == assignment.ID) // Filter graded submissions and not current student
             .GroupBy(s => s.UserID) // Group submissions by UserID
             .Select(group => new
             {
                 UserID = group.Key,
                 HighestGrade = group.Max(s => s.PointsEarned) // Calculate the highest grade for each group
             })
             .ToList();

            double total = 0;
            if (!highestGradesForEachUser.IsNullOrEmpty())
            {
                foreach (var item in highestGradesForEachUser)
                {
                    total += item.HighestGrade;
                }
            }
            AverageGradePer = ((total /  highestGradesForEachUser.Count)/assignment.maxPoints)*100;//getting percent
            


            return Page();
        }
    }
}
