using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using chief.DAL;
using Microsoft.EntityFrameworkCore;

namespace chief.Pages.ChecklistMaster
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Checklist Checklist { get; set; }

        public async Task<IActionResult> OnPostAsync(string ChecklistItems)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Parse ChecklistItems (comma-separated string)
            var items = ChecklistItems.Split(',', StringSplitOptions.RemoveEmptyEntries);
            Checklist.ChecklistItems = items.Select(item => new ChecklistItem { ItemName = item }).ToList();

            _context.Checklists.Add(Checklist);
            await _context.SaveChangesAsync();

            // Create a notification for checklist creation
            var notification = new Notify
            {
                Title = $"{Checklist.DocumentName} update",
                Message = $"The checklist for {Checklist.ApplicationType} has been created.",
                Status = "Created",
                DateCreated = DateTime.Now
            };
            _context.Notifs.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
