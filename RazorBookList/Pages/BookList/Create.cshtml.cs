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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookCreateModel Book { get; set; }

        public SelectList Authors { get; set; }

        public async Task OnGet()
        {
            var authors = await _context.Authors.ToListAsync();

            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Name = Book.Name,
                    Author = await _context.Authors.FindAsync(Book.AuthorId),
                    ISBN = Book.ISBN
                };

                await _context.AddAsync(book);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
