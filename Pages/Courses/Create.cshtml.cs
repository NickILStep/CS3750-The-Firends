using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment1v3.Data;
using Assignment1v3.Models;
using System.Security.Claims;
using System.Security.Principal;
using Stripe;
using Microsoft.Extensions.Primitives;

namespace Assignment1v3.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public List<SelectListItem> Items { get; set; }
        public Schools list = new Schools();

        public CreateModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Items = list.strings.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ToString(),
                                              Text = a
                                          }).ToList();
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;
        [BindProperty]
        public List<string> ClassDays {  get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int instID)
        {
            int instructorID;

            if (instID == null || instID == 0)
            {
                //This nasty line of code grabs the logged-in user, grabs their ID, and converts it to an integer for the DB
                instructorID = Convert.ToInt32((User.Claims.ElementAt(3).ToString()).Remove(0, 4));

                string daysList = "[";
                for (int i = 0; i < ClassDays.Count; i++)
                {
                    daysList += ClassDays[i].ToString();
                    if (i < ClassDays.Count - 1)
                    {
                        daysList += ", ";
                    }
                }
                daysList += "]";
                Course.ClassDays = daysList;
            }
            else
            {
                instructorID = instID;
            }

            Course.InstructorId = instructorID;

            if (!ModelState.IsValid || _context.Course == null || Course == null)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            var newevent = new Assignment1v3.Models.Event
            {
                title = Course.CourseNumber + ": " + Course.CourseName,
                startTime = Course.StartTime,
                endTime = Course.EndTime,
                startRecur = Course.StartRecur,
                endRecur = (Course.EndRecur).AddDays(1),
                daysOfWeek = Course.ClassDays,
                courseId = Course.Id,
                studSchedId = null,
                url = "/Home/InstructorDashboard",
            };

            if(instID == null || instID == 0)
            {
                newevent.userId = this.User.Claims.ElementAt(1).ToString();
            }
            else
            {
                newevent.userId = instID.ToString();
            }

            _context.Event.Add(newevent);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Home/InstructorDashboard");
        }
    }
}
