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
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
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

        public async Task<IActionResult> OnPostAsync(string ChecklistItems)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var existingItems = _context.ChecklistItems.Where(ci => ci.ChecklistId == Checklist.Id).ToList();
            _context.ChecklistItems.RemoveRange(existingItems);

            var newItems = ChecklistItems.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(item => new ChecklistItem { ItemName = item, ChecklistId = Checklist.Id }).ToList();

            _context.ChecklistItems.AddRange(newItems);
            _context.Attach(Checklist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();


            var notification = new Notify
            {
                Title = $"{Checklist.DocumentName} update",
                Message = $"The checklist for {Checklist.ApplicationType} has been updated.",
                Status = "Updated",
                DateCreated = DateTime.Now
            };
            _context.Notifs.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
