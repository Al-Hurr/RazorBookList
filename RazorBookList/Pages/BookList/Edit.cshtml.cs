using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookEditModel Book { get; set; }
        = new BookEditModel();

        public SelectList Authors { get; set; }

        public async Task OnGet(int id)
        {
            var authors = await _context.Authors.ToListAsync();
            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            var book = await _context.Books
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            Book.Id = book.Id;
            Book.Name = book.Name;
            Book.AuthorId = book.Author?.Id ?? default;
            Book.ISBN = book.ISBN;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Id = Book.Id,
                    Name = Book.Name,
                    Author = await _context.Authors.FindAsync(Book.AuthorId),
                    ISBN = Book.ISBN
                };

                _context.Update(book);

                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
