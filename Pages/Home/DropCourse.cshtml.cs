using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using System.Security.Claims;

namespace Assignment1v3.Pages.Home
{
    [Authorize(Policy = "MustBeStudent")]
    public class DropCourseModel : PageModel
    {
        private readonly Assignment1v3Context _context;

        public DropCourseModel(Assignment1v3Context context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;
 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.Id == id);
            System.Diagnostics.Debug.WriteLine($"Course: {course}");

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            /* Add this when the student Email gets fix in the studsched
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var studentEmail = userEmailClaim.Value;
            */
            //TEST:  var studentTest = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress: StudentTest@gmail.com";
            //TEST:  var courseNumber = 1234;

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var studentEmail = userEmailClaim.Value;
            var studentTest = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress: " + studentEmail;

            var courseToDrop = await _context.Course.FindAsync(id);

            if (courseToDrop != null)
            {
                // Check if the course is already in the student's schedule
                var existingEntry = await _context.StudSched
                    .FirstOrDefaultAsync(x => x.Email_Username == studentTest && x.CourseNum == courseToDrop.CourseNumber); //courseToDrop.CourseNumber or courseNumber

                if (existingEntry != null)
                {
                    // If the course is in the schedule, remove it
                    _context.StudSched.Remove(existingEntry);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./StudentDashboard"); // Redirect to the desired page after dropping the course
        }
    }
}
