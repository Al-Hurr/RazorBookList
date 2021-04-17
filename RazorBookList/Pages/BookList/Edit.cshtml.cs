using System;
using System.Collections.Generic;
using System.IO;
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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool IsStoresExists { get; set; }

        [BindProperty]
        public BookEditModel Book { get; set; }
        = new BookEditModel();

        [BindProperty]
        public int[] StoreIds { get; set; }

        public SelectList Authors { get; set; }
        public SelectList Stores { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var authors = await _context.Authors.ToListAsync();
            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            var stores = await _context.Store.ToListAsync();
            IsStoresExists = stores.Any();
            Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

            StoreIds = await _context.RelStoreBook
                .Where(x => x.Book.Id == id)
                .Select(x => x.Store.Id)
                .ToArrayAsync();

            var book = await _context.Books
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            Book.Id = book.Id;
            Book.Name = book.Name;
            Book.AuthorId = book.Author?.Id ?? default;
            Book.ISBN = book.ISBN;

            return Page();
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

                if (Book.Image != null)
                {
                    if (Book.Image.Length > 0)
                    {
                        byte[] imageData = null;

                        using (var binaryReader = new BinaryReader(Book.Image.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)Book.Image.Length);
                        }

                        book.Cover = imageData;
                    }
                }
                else
                {
                    var item = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == book.Id);
                    book.Cover = item.Cover;
                }

                _context.Update(book);

                if (StoreIds.Length != 0)
                {
                    var bookStores = _context.RelStoreBook.Where(x => x.Book.Id == book.Id);

                    if (bookStores.Any())
                    {
                        _context.RelStoreBook.RemoveRange(bookStores);
                    }

                    for (int i = 0; i < StoreIds.Length; i++)
                    {
                        var BookStore = new RelStoreBook()
                        {
                            Book = book,
                            Store = await _context.Store.FindAsync(StoreIds[i])
                        };

                        await _context.AddAsync(BookStore);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return NotFound("Model is not valid");
        }
    }
}
