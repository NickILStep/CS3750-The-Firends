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
using Stripe;

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

        public List<Course> Course { get; set; }
        public List<Assignment> TO_DO { get; set; }


        public async Task OnGetAsync()
        {
            /* Old Code
            Course = new List<Course>();
            TO_DO = new List<Assignment>();

            // Get the currently authenticated user's email address claim
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            //System.Diagnostics.Debug.WriteLine(user);
            if (userEmailClaim != null)
            {
                var userEmailClaimValue = userEmailClaim.Value;
                var studentCourses = await _context.StudSched.Where(x => x.Email_Username.Contains(userEmailClaimValue)).ToListAsync();
                //var studentCourses = await _context.StudSched.Where(x => x.ide.Contains(userEmailClaimValue)).ToListAsync();
                //System.Diagnostics.Debug.WriteLine(studentCourses);

                foreach (var tempcourse in studentCourses)
                {
                    var matchingCourses = _context.Course.Where(x => x.CourseNumber == tempcourse.CourseNum).ToList();

                    if (matchingCourses.Count > 0)
                    {
                        Course myCourse = matchingCourses[0];
                        Course.Add(myCourse);
                    }

                }
            */
            Course = new List<Course>();
            TO_DO = new List<Assignment>();

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (userEmailClaim != null)
            {
                var userEmailClaimValue = userEmailClaim.Value;

                // Eager loading to include related Course entities
                var studentCourses = await _context.StudSched
                    .Where(x => x.Email_Username.Contains(userEmailClaimValue))
                    //.Include(s => s.CourseId) // Eager loading
                    .ToListAsync();

                foreach (var tempcourse in studentCourses)
                {
                    // Access the CourseId without additional queries
                    int? courseId = tempcourse.CourseId;

                    if (courseId.HasValue)
                    {
                        // Fetch the associated Course directly using the CourseId
                        var matchingCourse = _context.Course.Find(courseId);

                        if (matchingCourse != null)
                        {
                            // Add the Course to your list
                            Course.Add(matchingCourse);
                        }
                    }
                }


                foreach (var tempcourse in studentCourses)//TODO section
                {


                    var matchingAssignments = await (
                        from assignment in _context.Assignment
                        join course in _context.Course on assignment.course equals course.Id // to access name and number later
                        where assignment.course == tempcourse.CourseId && //from a course enrolled in
                              assignment.startDate <= DateTime.Now && //assignment is open
                              assignment.dueDate >= DateTime.Now && //still to be due
                              !_context.Submission.Any(s => s.AssignmentID == assignment.ID && s.UserID == int.Parse(this.User.Claims.ElementAt(3).Value)) // where it hasn't been submitted yet.
                        select new Assignment
                        {
                            ID = assignment.ID,
                            description = course.CourseNumber.ToString() + "-" + course.CourseName.ToString(), //this is wonky but I'm stealing the description slot to access the courseNumber and Course name
                            name = assignment.name,
                            maxPoints = assignment.maxPoints,
                            startDate = assignment.startDate,
                            dueDate = assignment.dueDate
                        }).ToListAsync();
                    TO_DO.AddRange(matchingAssignments);

                }
                TO_DO = TO_DO.OrderBy(x => x.dueDate).ToList();
                // System.Diagnostics.Debug.WriteLine(Course);

            }

        }

    }
}