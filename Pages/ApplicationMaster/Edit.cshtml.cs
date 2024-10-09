﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chief.DAL;

namespace chief.Pages.ApplicationMaster
{
    public class EditModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public EditModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application =  await _context.Applications.FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            Application = application;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            // Update the DaysToEvaluate field with the form value
            application.DaysToEvaluate = Application.DaysToEvaluate;

            // Apply the update to all other applications
            var allApplications = await _context.Applications.ToListAsync();
            foreach (var app in allApplications)
            {
                app.DaysToEvaluate = application.DaysToEvaluate;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
