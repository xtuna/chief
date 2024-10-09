using chief.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chief.Pages.EvaluatorMaster
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProfileModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Evaluator Evaluator { get; set; } = default!;

        public int PendingRequests { get; set; }
        public int EvaluatedRequests { get; set; }
        public int MissedRequests { get; set; }

        public IList<EvaluatorDocument> EvaluatorDocuments { get; set; } = new List<EvaluatorDocument>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve evaluator details based on the ID
            Evaluator = await _context.Evaluators.FindAsync(id);

            if (Evaluator == null)
            {
                return NotFound();
            }

            // Calculate the counts of different request types
            PendingRequests = await _context.Applications.CountAsync(a => a.EvaluatorId == id && a.Status == "Pending");
            EvaluatedRequests = await _context.Applications.CountAsync(a => a.EvaluatorId == id && a.Status == "Evaluated");
            MissedRequests = await _context.Applications.CountAsync(a => a.EvaluatorId == id && a.Status == "Missed");

            // Retrieve related documents for the evaluator
            EvaluatorDocuments = await _context.EvaluatorDocuments
                .Where(d => d.EvaluatorId == id)
                .ToListAsync();

            return Page();
        }
    }
}
