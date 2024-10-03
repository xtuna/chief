using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.ChecklistMaster
{
    public class EditModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public EditModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Checklist Checklist { get; set; } = new Checklist { ChecklistItems = new List<ChecklistItem>() };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Load the checklist and its items
            Checklist = await _context.Checklists
                .Include(c => c.ChecklistItems) // Include checklist items
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Checklist == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var checklistToUpdate = await _context.Checklists
        .Include(c => c.ChecklistItems)
        .FirstOrDefaultAsync(c => c.Id == Checklist.Id);

            if (checklistToUpdate == null)
            {
                return NotFound();
            }

            checklistToUpdate.DocumentName = Checklist.DocumentName;
            checklistToUpdate.ApplicationType = Checklist.ApplicationType;

            checklistToUpdate.ChecklistItems.Clear();
            checklistToUpdate.ChecklistItems.AddRange(Checklist.ChecklistItems);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
