using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment1v3.Pages
{
    [Authorize(Policy = "MustBeStudent")]
    public class NotificationModel : PageModel
    {
        private readonly Data.Assignment1v3Context _context;
        
        int countsub = 0;
        public NotificationModel(Data.Assignment1v3Context context)
        {
            _context = context;
        }
        
        public List<Submission> Submission { get; set; } 

        public List<Submission> add { get; set; } = default!;
        
        public List<Assignment> Assignment { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync()
        {
            Submission = new List<Submission>();
            Assignment = new List<Assignment>();
            int? studentid = 0;
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (_context.Submission == null || _context.Assignment == null)
            {
                return NotFound();
            }
            if (userEmailClaim != null)
            {
                var userEmailClaimValue = userEmailClaim.Value;
                var studentCourses = await _context.StudSched.Where(x => x.Email_Username.Contains(userEmailClaimValue)).ToListAsync();
                foreach (var tempcourse in studentCourses)//TODO section
                {
                    int count2 = 0;
                    studentid = tempcourse.StudId;
                    var matchingAssignments = _context.Assignment.Where(a=> a.course == tempcourse.CourseId && a.created_date <= DateTime.Now && a.created_date >= DateTime.Now.AddDays(-1)).ToList();
                        
                    foreach (var assignment in matchingAssignments)
                    {
                        Assignment assigntest = matchingAssignments[count2];
                        Assignment.Add(assigntest);
                        count2++;
                    }

                  
                }
                var Submissionlist = _context.Submission
               .Where(a => a.UserID == studentid && a.Graded == true && a.modified_date <= DateTime.Now && a.modified_date >= DateTime.Now.AddDays(-1)).ToList();
                foreach (var submission in Submissionlist)
                {
                    Submission subtest = Submissionlist[countsub];
                    Submission.Add(subtest);
                    countsub++;
                }
            }
                await _context.SaveChangesAsync();
            return Page();
        }
        
    }
}
