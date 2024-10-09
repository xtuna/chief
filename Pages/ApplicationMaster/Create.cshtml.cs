using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using chief.DAL;

namespace chief.Pages.ApplicationMaster
{
    public class CreateModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public CreateModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Applications.Add(Application);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
