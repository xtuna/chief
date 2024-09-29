using chief.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace chief.Pages.ApplicationMaster
{
    public class EndorseModel : PageModel
    {
        private readonly AppDbContext _context;

        public EndorseModel(AppDbContext context)
        {
            _context = context;
        }

        // Bind the uploaded file from the form
        [BindProperty]
        public IFormFile UploadedFile { get; set; } // This is the key part for file upload

        [BindProperty]
        public Application Application { get; set; } // Assuming you are working with the Application entity

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Application = await _context.Applications.FindAsync(id);

            if (Application == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            // Save the uploaded file if there is one
            if (UploadedFile != null)
            {
                // Save the uploaded file to the server
                var filePath = Path.Combine("wwwroot/uploads", UploadedFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                // Save the file name in the application record
                application.UploadedFileName = UploadedFile.FileName;

                // Update the application status to "Endorsed" or any other necessary logic
                application.Status = "Endorsed";
                _context.Attach(application).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            // Redirect to another page or show success message
            return RedirectToPage("/ApplicationMaster");
        }

    }
}
