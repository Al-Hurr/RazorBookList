using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; }

        public bool IsRelated { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Author = await _context.Authors
                .FindAsync(id.Value);

            if (Author == null)
                return NotFound();

            IsRelated = await _context.Books.AnyAsync(x => x.Author.Id == Author.Id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var author = await _context.Authors.FindAsync(id.Value);

            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
