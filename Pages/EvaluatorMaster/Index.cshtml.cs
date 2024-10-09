using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.EvaluatorMaster
{
    public class IndexModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public IndexModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Evaluator> Evaluator { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Evaluator = (IList<Evaluator>)await _context.Evaluators.ToListAsync();
        }
    }
}
