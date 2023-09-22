using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;

namespace Assignment1v3.Pages.Logins
{
    public class IndexModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public IndexModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Login> Login { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Login != null)
            {
                Login = await _context.Login.ToListAsync();
            }
        }
    }
}
