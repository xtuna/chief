using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.NotifyMaster
{
    public class DeleteModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public DeleteModel(chief.DAL.AppDbContext context)
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

            var notify = await _context.Notifs.FirstOrDefaultAsync(m => m.Id == id);

            if (notify == null)
            {
                return NotFound();
            }
            else
            {
                Notify = notify;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notify = await _context.Notifs.FindAsync(id);
            if (notify != null)
            {
                Notify = notify;
                _context.Notifs.Remove(Notify);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
