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

        public async Task OnGetAsync()
        {
            var applications = await _context.Applications.ToListAsync();
            PendingApplications = applications.Where(a => a.Status == "Pending" || a.Status == "Endorsed").ToList();
            CompletedApplications = applications.Where(a => a.Status == "Approved" || a.Status == "Rejected").ToList();
        }

        public async Task<IActionResult> OnPostEndorseAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Endorsed";

            _context.Attach(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application has been endorsed.";

            return RedirectToPage(); 
        }
        [HttpPost]
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

        private IActionResult Ok(object value)
        {
            throw new NotImplementedException();
        }
        public class UpdateDaysRequest
        {
            public int Id { get; set; }
            public int DaysToEvaluate { get; set; }
        }

    }
}
