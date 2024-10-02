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
        private readonly chief.DAL.AppDbContext _context;

        public IndexModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Checklist> Checklist { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Checklist = await _context.Checklists.ToListAsync();
        }
    }
}
