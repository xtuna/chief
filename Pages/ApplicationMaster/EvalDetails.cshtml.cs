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

        // Nullable to avoid CS8618 warning, assigned after fetching from the database
        [BindProperty]
        public Application? Application { get; set; }  // Make Application nullable

        public string? Status { get; set; }  // Make Status nullable

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the application based on ID
            Application = await _context.Applications.FindAsync(id);

            if (Application == null)
            {
                return NotFound();
            }

            Status = Application.Status; // Safely assign Status after fetching
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

            TempData["Message"] = "Application status updated successfully";  // Feedback to user

            return RedirectToPage("/ApplicationMaster/Index");
        }
    }
}
