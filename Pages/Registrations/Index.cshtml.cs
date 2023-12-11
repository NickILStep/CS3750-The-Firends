using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment1v3.Pages.Registrations
{
    [Authorize(policy: "MustBeStudent")]
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
        public string DeptSort { get; set; }
        public List<SelectListItem> Items { get; set; }

        public IList<StudSched> studScheds { get; set; } = default!;
        public IList<Course> Course { get; set; } = default!;

        public IList<StudSched> StudSched { get; set; } = default!;

        [BindProperty]
        public StudSched Stud { get; set; } = default!;
        [BindProperty]
        public Course course { get; set; }


        public async Task OnGetAsync(string sortOrder, string searchString, string deptOrder)
        {
            Schools list = new Schools();
            Items = list.strings.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ToString(),
                                              Text = a
                                          }).ToList();

            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            DeptSort = deptOrder;

            CurrentFilter = searchString;
            studScheds = await _context.StudSched.ToListAsync();

            IQueryable<Course> coursesIQ = from s in _context.Course
                                           select s;
            if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(deptOrder))
            {
                if (searchString == null)
                {
                    coursesIQ = coursesIQ.Where(c => c.School.Contains(deptOrder));
                }
                else
                {
                    coursesIQ = coursesIQ.Where(s => s.CourseName.Contains(searchString)
                                       || s.Description.Contains(searchString));
                }
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
        public async Task<IActionResult> OnPostAsync(StudSched sched)
        {
            var newsched = new StudSched();
            if (sched.StudId == null || sched.StudId == 0)
            {
                /// = Request.Cookies["AuthCookie"];
                newsched.Email_Username = this.User.Claims.ElementAt(1).ToString();
                newsched.CourseNum = course.CourseNumber;
                newsched.StudId = int.Parse(this.User.Claims.ElementAt(3).Value);
                newsched.CourseId = course.Id;
            }
            else
            {
                newsched = sched;
            }
            _context.StudSched.Add(newsched);

            await _context.SaveChangesAsync();

            Event newevent = new Event
            {
                title = course.CourseNumber + ": " + course.CourseName,
                startTime = course.StartTime,
                endTime = course.EndTime,
                startRecur = course.StartRecur,
                endRecur = course.EndRecur,
                daysOfWeek = course.ClassDays,
                courseId = course.Id,
                studSchedId = newsched.Id,
                url = "/Assignments/StudentCourseView?courseId=" + course.Id.ToString(),
            };
            if (sched.StudId == null || sched.StudId == 0)
            {
                newevent.userId = this.User.Claims.ElementAt(1).ToString();
            }
            else
            {
                newevent.userId = sched.StudId.ToString();
            }
            _context.Event.Add(newevent);

            await _context.SaveChangesAsync();
            return RedirectToPage("/Registrations/Index");
        }

    }
}

