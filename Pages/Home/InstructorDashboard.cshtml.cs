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

        public async Task OnGetAsync()
        {
            Course = new List<Course>();

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
                }
            }
        }

    }
}