using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;

namespace Assignment1v3.Pages.Registrations
{
    public class IndexModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public IndexModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Course> Course { get; set; } = default!;

        public IList<StudSched> StudSched { get; set; }

        public async Task OnGetAsync(string sortOrder,  string searchString)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;
            //Course = await _context.Course.ToListAsync();

            IQueryable<Course> coursesIQ = from s in _context.Course
                                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                coursesIQ = coursesIQ.Where(s => s.CourseName.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    coursesIQ = coursesIQ.OrderByDescending(s => s.CourseName);
                    break;
                case "Date":
                    coursesIQ = coursesIQ.OrderBy(s => s.StartTime);
                    break;
                case "date_desc":
                    coursesIQ = coursesIQ.OrderByDescending(s => s.StartTime);
                    break;
                default:
                    coursesIQ = coursesIQ.OrderBy(s => s.CourseNumber);
                    break;
            }

            Course = await coursesIQ.AsNoTracking().ToListAsync();
        }

    }
}
