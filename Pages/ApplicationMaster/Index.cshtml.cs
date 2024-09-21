using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.ApplicationMaster
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Application> PendingApplications { get; set; } = new List<Application>();  // Initialize with empty list
        public IList<Application> CompletedApplications { get; set; } = new List<Application>();  // Initialize with empty list


        public async Task OnGetAsync()
        {
            // Fetch all applications from the database
            var applications = await _context.Applications.ToListAsync();

            // Filter applications based on status
            PendingApplications = applications.Where(a => a.Status == "Pending").ToList();
            CompletedApplications = applications.Where(a => a.Status == "Approved" || a.Status == "Rejected").ToList();
        }
    }
}
