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
        public async Task<IActionResult> UpdateDaysToEvaluate([FromBody] UpdateDaysRequest request)
        {
            // Validate the request object
            if (request == null || request.Id <= 0 || request.DaysToEvaluate <= 0)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            // Fetch the application by ID
            var application = await _context.Applications.FindAsync(request.Id);
            if (application == null)
            {
                return NotFound(new { message = "Application not found." });
            }

            // Update the DaysToEvaluate field
            application.DaysToEvaluate = request.DaysToEvaluate;
            await _context.SaveChangesAsync();

            // Return a success response using Ok()
            return Ok(new { message = "Days to Evaluate updated successfully." });
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
