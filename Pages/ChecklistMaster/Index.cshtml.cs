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
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Checklist> Checklist { get; set; }

        public async Task OnGetAsync()
        {
            Checklist = await _context.Checklists
                .Include(c => c.ChecklistItems)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist != null)
            {
                _context.Checklists.Remove(checklist);
                await _context.SaveChangesAsync();

                // Create push notification for delete action
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

            return RedirectToPage();
        }

        private void CreateNotification(string title, string message)
        {
            var notification = new Notify
            {
                Title = title + " update",
                Message = message,
                Status = "Unread",
                DateCreated = DateTime.Now
            };

            _context.Notifs.Add(notification);
            _context.SaveChanges();
        }
    }
}
