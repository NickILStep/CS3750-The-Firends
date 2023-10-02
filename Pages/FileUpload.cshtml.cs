using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Assignment1v3.Pages
{
    public class FileUploadModel : PageModel
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public FileUploadModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/profilePictures", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
        }
    }
}
