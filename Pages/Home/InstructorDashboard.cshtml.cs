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

        public IList<InstructorCourse> InstructorCourses { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch courses associated with the current instructor
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmailClaim != null)
            {
                var userEmail = userEmailClaim.Value;
                InstructorCourses = await _context.InstructorCourse
                    .Include(ic => ic.Course)
                    .Where(ic => ic.Instructor.Email_Username == userEmail)
                    .ToListAsync();
            }
        }
    }
}
