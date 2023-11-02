using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;

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

        public IList<Course> Course { get; set; }

        public async Task OnGetAsync()
        {
            // Get the currently authenticated user's email or username
            var user = User.Identity.Name;

            // Query the StudSched table to get the courses for the authenticated user
            var registeredCourses = await _context.StudSched
                .Where(s => s.Email_Username == user)
                .Select(s => s.CourseNum)
                .ToListAsync();

            // Query the Course table to get the course details for the registered courses
            Course = await _context.Course
                .Where(c => registeredCourses.Contains(c.CourseNumber))
                .ToListAsync();
        }
    }
}
