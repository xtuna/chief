using chief.DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chief.Pages.ApplicationMaster
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Application> PendingApplications { get; set; } = new List<Application>();
        public IList<Application> CompletedApplications { get; set; } = new List<Application>();

        // OnGet method
        public async Task OnGetAsync()
        {
            var applications = await _context.Applications.ToListAsync();
            PendingApplications = applications.Where(a => a.Status == "Pending" || a.Status == "Endorsed").ToList();
            CompletedApplications = applications.Where(a => a.Status == "Approved" || a.Status == "Rejected").ToList();
        }

        // OnPostEndorseAsync method
        public async Task<IActionResult> OnPostEndorseAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            // Perform endorsement action
            application.Status = "Endorsed";

            _context.Attach(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application has been endorsed.";

            return RedirectToPage(); // Redirect back to the Index page
        }
        [HttpPost]
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

        private IActionResult Ok(object value)
        {
            throw new NotImplementedException();
        }

        // Create a request class for validation purposes
        public class UpdateDaysRequest
        {
            public int Id { get; set; }
            public int DaysToEvaluate { get; set; }
        }

    }
}