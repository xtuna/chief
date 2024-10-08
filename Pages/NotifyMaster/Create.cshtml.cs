﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using chief.DAL;

namespace chief.Pages.NotifyMaster
{
    public class CreateModel : PageModel
    {
        private readonly chief.DAL.AppDbContext _context;

        public CreateModel(chief.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Notify Notify { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notifs.Add(Notify);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
