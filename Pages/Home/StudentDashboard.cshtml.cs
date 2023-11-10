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

        public List<Course> Course { get; set; }

        public async Task OnGetAsync()
        {
            Course = new List<Course>();

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

               // System.Diagnostics.Debug.WriteLine(Course);

            }
        }

    }
}