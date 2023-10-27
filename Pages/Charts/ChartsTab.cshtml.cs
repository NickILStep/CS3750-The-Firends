using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages.Charts
{
    [Authorize]
    public class ChartsTabModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;


        public ChartsTabModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Login Login { get; set; } = default!;
        //[BindProperty]
        //public Course Course { get; set; } = default!;
        public List<int> CourseNumbers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            CourseNumbers = await _context.Course.Select(Course => Course.CourseNumber).ToListAsync();//gets list of courseNumbers

            //checks userID in cookie
            if (this.User.Claims.ElementAt(3) == null)
            {
                return NotFound();
            }
            //sets login to data from database
            var login = await _context.Login.FirstOrDefaultAsync(m => m.Id == int.Parse(this.User.Claims.ElementAt(3).Value));
            if (login == null)
            {
                return NotFound();
            }
            //binds data
            Login = login;

            
            return Page();
        }

        public IActionResult OnPost()
        {
            return NotFound();
        }


        public string GetCourseName(int courseNumber)
        {
            var course = _context.Course.FirstOrDefault(c => c.CourseNumber == courseNumber);
            return course?.CourseName ?? "Unknown Course";
        }
    }
}
