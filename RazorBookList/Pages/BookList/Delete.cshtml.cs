using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.BookList
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Book = await _context.Books
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id.Value);

            if (Book == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var book = await _context.Books.FindAsync(id.Value);

            if (book == null)
                return NotFound();

            if(await _context.RelStoreBook.AnyAsync(x => x.Book.Id == book.Id))
            {
                var bookStores = _context.RelStoreBook.Where(x => x.Book.Id == book.Id);

                _context.RelStoreBook.RemoveRange(bookStores);
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
