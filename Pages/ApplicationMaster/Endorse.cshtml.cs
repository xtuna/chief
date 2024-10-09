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
            Application = await _context.Applications
                .Include(a => a.ApplicationFiles)  // Include ApplicationFiles
                .FirstOrDefaultAsync(a => a.Id == id);

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

            if (UploadedFile != null)
            {
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var extension = Path.GetExtension(UploadedFile.FileName);
                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    ModelState.AddModelError("UploadedFile", "Invalid file type. Please upload a PDF or DOCX file.");
                    return Page(); // Return to the page with error messages
                }
                // Save the uploaded file to the server
                var uniqueFileName = $"{Guid.NewGuid()}_{UploadedFile.FileName}";
                var filePath = Path.Combine("wwwroot/uploads", UploadedFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                application.UploadedFileName = UploadedFile.FileName;
            }

            // Update application status
            application.Status = "Endorsed";
            _context.Attach(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("/ApplicationMaster");
        }
        public async Task<IActionResult> OnPostUpdateDaysToEvaluateAsync(int id, int daysToEvaluate)
        {
            // Find the application by ID
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            // Update DaysToEvaluate for this application
            application.DaysToEvaluate = daysToEvaluate;

            // Optionally: Update the value for all related applications
            var relatedApplications = _context.Applications.Where(a => a.Status == "Active");
            foreach (var app in relatedApplications)
            {
                app.DaysToEvaluate = daysToEvaluate; // Update all active applications
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Optionally, send success response
            return new JsonResult(new { success = true });
        }

    }
}