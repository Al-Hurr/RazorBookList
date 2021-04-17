using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        [BindProperty]
        public IFormFile Img { get; set; }

        public async Task OnGet()
        {
            Books = await _context.Books
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var book = await _context.Books.FindAsync(id.Value);

            if(book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToPage();
        }

    }
}
