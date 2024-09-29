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
    public class DetailsModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public DetailsModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
