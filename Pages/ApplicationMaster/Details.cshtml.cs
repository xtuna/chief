using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.ApplicationMaster
{
    public class DetailsModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public DetailsModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            else
            {
                Application = application;
            }
            return Page();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdateDaysToEvaluate([FromBody] UpdateDaysRequest request)
        {
            var application = await _context.Applications.FindAsync(request.Id);
            if (application == null)
            {
                return NotFound();
            }

            application.DaysToEvaluate = request.DaysToEvaluate;
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public class UpdateDaysRequest
        {
            public int Id { get; set; }
            public int DaysToEvaluate { get; set; }
        }
    }
}
