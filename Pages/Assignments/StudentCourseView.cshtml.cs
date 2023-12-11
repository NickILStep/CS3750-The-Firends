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
        
        public double YourPercent { get; set; }
        public double AveragePercent { get; set; }
        public decimal Grade { get; set; }
        public string LetterGrade {  get; set; }

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

            double maxPointTotal = 0;

            foreach (var assignment in Assignment)
            {
                var highestGrade = await _context.Submission
                    .Where(s => s.AssignmentID == assignment.ID && s.UserID == StudentID)
                    .MaxAsync(s => (int?)s.PointsEarned);
                maxPointTotal += assignment.maxPoints;
                if(highestGrade != null)
                {
                    YourPercent += (double)highestGrade;
                }
                
                HighestGrades[assignment.ID] = highestGrade;
            }

            YourPercent = (YourPercent / maxPointTotal) * 100;
            AveragePercent = 0;
            maxPointTotal = 0;
            var students = await _context.StudSched.Where(s => s.CourseId == courseId).ToListAsync();
            foreach (var student in students)
            {
                foreach (var Oassignment in Assignment)
                {
                    var OhighestGrade = await _context.Submission
                        .Where(s => s.AssignmentID == Oassignment.ID && s.UserID == student.StudId)
                        .MaxAsync(s => (int?)s.PointsEarned);  
                    maxPointTotal += Oassignment.maxPoints;
                    if(OhighestGrade != null)
                    {
                        AveragePercent += (double)OhighestGrade;
                    }
                
                }

            }
            

            AveragePercent = (AveragePercent / maxPointTotal) * 100;

            if(YourPercent > 0)
            {
                decimal temp = (Convert.ToDecimal(YourPercent));
                Grade = Math.Round(temp, 2);
                SetLetterGrade();
            }
            

            return Page();
        }

        public void SetLetterGrade()
        {
            if(Grade >= 94)
            {
                LetterGrade = "A";
            }
            else if (Grade > 90 && Grade < 94)
            {
                LetterGrade = "A-";
            }
            else if (Grade > 87 && Grade < 90)
            {
                LetterGrade = "B+";
            }
            else if (Grade > 84 && Grade < 87)
            {
                LetterGrade = "B";
            }
            else if (Grade > 80 && Grade < 84)
            {
                LetterGrade = "B-";
            }
            else if (Grade > 77 && Grade < 80)
            {
                LetterGrade = "C+";
            }
            else if (Grade > 74 && Grade < 77)
            {
                LetterGrade = "C";
            }
            else if (Grade > 70 && Grade < 74)
            {
                LetterGrade = "C-";
            }
            else if (Grade > 67 && Grade < 70)
            {
                LetterGrade = "D+";
            }
            else if (Grade > 64 && Grade < 67)
            {
                LetterGrade = "D";
            }
            else if (Grade > 60 && Grade < 64)
            {
                LetterGrade = "D-";
            }
            else
            {
                LetterGrade = "F";
            }
        }
    }
}
