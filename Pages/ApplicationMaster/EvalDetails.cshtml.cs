using chief.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace chief.Pages.ApplicationMaster
{
    public class EvalDetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public EvalDetailsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application? Application { get; set; }

        public string? Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // fetch application ID
            Application = await _context.Applications.FindAsync(id);

            if (Application == null)
            {
                return NotFound();
            }

            Status = Application.Status; // assign Status after fetching
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string status)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            if (status == "Approve")
            {
                application.Status = "Approved";
            }
            else if (status == "Reject")
            {
                application.Status = "Rejected";
            }

            _context.Attach(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application status updated successfully";

            return RedirectToPage("/ApplicationMaster/Index");
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
