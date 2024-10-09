using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.NotifyMaster
{
    public class EditModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public EditModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notify Notify { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notify =  await _context.Notifs.FirstOrDefaultAsync(m => m.Id == id);
            if (notify == null)
            {
                return NotFound();
            }
            Notify = notify;
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Notify).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifyExists(Notify.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotifyExists(int id)
        {
            return _context.Notifs.Any(e => e.Id == id);
        }
    }
}
