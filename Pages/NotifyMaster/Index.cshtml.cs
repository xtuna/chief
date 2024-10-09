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
    public class IndexModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public IndexModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Notify> Notify { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // You can add sorting/filtering logic here by userId, role, etc.
            Notify = await _context.Notifs
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
        }
    }
}
