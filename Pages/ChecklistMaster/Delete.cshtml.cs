using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.ChecklistMaster
{
    public class DeleteModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public DeleteModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Checklist Checklist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklists.FirstOrDefaultAsync(m => m.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }
            else
            {
                Checklist = checklist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist != null)
            {
                Checklist = checklist;
                _context.Checklists.Remove(Checklist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
