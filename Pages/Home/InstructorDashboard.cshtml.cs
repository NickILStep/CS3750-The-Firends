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
using NuGet.Packaging;

namespace Assignment1v3.Pages.Home
{
    [Authorize(Policy = "MustBeInstructor")]
    public class InstructorDashboardModel : PageModel
    {
        private readonly Assignment1v3Context _context;

        public InstructorDashboardModel(Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; }
        public List<Submission> TO_DO { get; set; }

        public async Task OnGetAsync()
        {
            Course = new List<Course>();
            TO_DO = new List<Submission>();

            // Get the currently authenticated user's email claim
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            System.Diagnostics.Debug.WriteLine(userEmailClaim);

            if (userEmailClaim != null)
            {
                var userEmail = userEmailClaim.Value;

                // Find the user by email address
                var user = await _context.Login
                    .FirstOrDefaultAsync(u => u.Email_Username == userEmail);
                System.Diagnostics.Debug.WriteLine(user);

                if (user != null)
                {
                    var userId = user.Id;
                    System.Diagnostics.Debug.WriteLine(userId);

                    // Find courses where the instructor ID matches the user's ID
                    var instructorCourses = await _context.Course
                        .Where(c => c.InstructorId == userId)
                        .ToListAsync();

                    Course.AddRange(instructorCourses);
                


                foreach (var tempcourse in instructorCourses)//TODO section
                    {


                        var matchingSubmissions = await (
                            from Submission in _context.Submission
                            join Assignment in _context.Assignment on Submission.AssignmentID equals Assignment.ID // to access name and number later
                            join Course in _context.Course on Assignment.course equals Course.Id
                            join Login in _context.Login on Submission.UserID equals Login.Id
                            where //from a course they teach                                   
                                  Assignment.startDate <= DateTime.Now &&   //Assignment has started
                                  Submission.Graded == false  //assignment is ungraded
                            select new Submission
                            {
                               AssignmentID = Submission.AssignmentID,
                               UserID = Submission.UserID,
                               maxPoints = Submission.maxPoints,
                               submissionType = Submission.submissionType,
                               ID = Submission.ID,
                               TextBox = Course.CourseNumber.ToString() + "-" + Course.CourseName.ToString(), //this is wonky but I'm stealing the Textbox slot to access the courseNumber and Course name
                                Upload = Login.Name_First.ToString() + " " + Login.Name_Last.ToString() + " - " + Assignment.name.ToString() //this is wonky but I'm stealing the Upload slot to access the Student name and Assignment name
                            }).ToListAsync();
                        TO_DO.AddRange(matchingSubmissions);

                    }
                    TO_DO = TO_DO.OrderBy(x => x.TextBox).ToList();

                }


            }

            
        }

    }
}