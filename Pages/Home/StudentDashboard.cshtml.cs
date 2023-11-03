using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Assignment1v3.Pages.Home
{
    [Authorize(Policy = "MustBeStudent")]
    public class StudentDashboardModel : PageModel
    {
        private readonly Assignment1v3Context _context;

        public StudentDashboardModel(Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<StudentCourseViewModel> Course { get; set; }
        public async Task OnGetAsync()
        {
            Course = new List<StudentCourseViewModel>(); // Initialize the list of courses

            // Get the currently authenticated user's email address claim
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (userEmailClaim != null)
            {
                var userEmailClaimValue = userEmailClaim.Value;

                // Use a regular expression to extract the email address
                var match = Regex.Match(userEmailClaimValue, ":(.*)$"); // Match everything after the colon
                if (match.Success)
                {
                    // Extract the email address part and trim any leading or trailing spaces
                    var userEmail = match.Groups[1].Value.Trim();

                    // Use userEmail to query the database for the student's courses
                    Course = await _context.Login
                        .Where(u => u.Email_Username == userEmail)
                        .Join(
                            _context.StudSched,
                            user => user.Email_Username,
                            studSched => studSched.Email_Username,
                            (user, studSched) => new { User = user, CourseNum = studSched.CourseNum }
                        )
                        .Join(
                            _context.Course,
                            combined => combined.CourseNum,
                            course => course.CourseNumber,
                            (combined, course) => new StudentCourseViewModel
                            {
                                UserFirstName = combined.User.Name_First,
                                UserLastName = combined.User.Name_Last,
                                CourseName = course.CourseName,
                                Location = course.Location,
                                StartTime = course.StartTime
                            }
                        )
                        .ToListAsync();
                }
            }
        }
    }
}
