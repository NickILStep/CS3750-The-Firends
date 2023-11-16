using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeInstructor")]
    public class CreateModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;
        public List<SelectListItem> Items { get; set; }

        public CreateModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Items = _context.Course.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.CourseName.ToString(),
                                              Text = a.CourseName
                                          }).ToList();
            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Assignment == null || Assignment == null)
            {
                return Page();
            }

            var courseID = _context.Course.Where(a => a.Id == Assignment.course).FirstOrDefaultAsync();
            Assignment.course = courseID.Result.Id;

            _context.Assignment.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}