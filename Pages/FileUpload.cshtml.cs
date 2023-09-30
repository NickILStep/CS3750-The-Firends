using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Assignment1v3.Pages
{
    public class FileUploadModel : PageModel
    {
        [BindProperty]
        public IFormFile Photo { get; set; }
        public void OnGet()
        {
        }
    }
}
