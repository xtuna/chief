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

        [BindProperty]
        public IFormFile UploadedFile { get; set; } 

        [BindProperty]
        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Application = await _context.Applications
                .Include(a => a.ApplicationFiles)
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
                    return Page();
                }
                var uniqueFileName = $"{Guid.NewGuid()}_{UploadedFile.FileName}";
                var filePath = Path.Combine("wwwroot/uploads", UploadedFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                application.UploadedFileName = UploadedFile.FileName;
            }

            application.Status = "Endorsed";
            _context.Attach(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("/ApplicationMaster");
        }
        public async Task<IActionResult> OnPostUpdateDaysToEvaluateAsync(int id, int daysToEvaluate)
        {

            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            application.DaysToEvaluate = daysToEvaluate;

            var relatedApplications = _context.Applications.Where(a => a.Status == "Active");
            foreach (var app in relatedApplications)
            {
                app.DaysToEvaluate = daysToEvaluate;
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

    }
}
