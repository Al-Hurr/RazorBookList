using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Author = await _context.Authors.FindAsync(id);

            if (Author == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Update(Author);

                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
