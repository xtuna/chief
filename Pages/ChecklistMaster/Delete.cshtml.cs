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
        public Checklist Checklist { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Checklist = await _context.Checklists.Include(c => c.ChecklistItems).FirstOrDefaultAsync(c => c.Id == id);

            if (Checklist == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var checklist = await _context.Checklists.Include(c => c.ChecklistItems).FirstOrDefaultAsync(c => c.Id == id);

            if (checklist != null)
            {
                _context.Checklists.Remove(checklist);
                _context.ChecklistItems.RemoveRange(checklist.ChecklistItems);
                await _context.SaveChangesAsync();

                // Create a notification for checklist deletion
                var notification = new Notify
                {
                    Title = $"{checklist.DocumentName} update",
                    Message = $"The checklist for {checklist.ApplicationType} has been deleted.",
                    Status = "Deleted",
                    DateCreated = DateTime.Now
                };
                _context.Notifs.Add(notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
