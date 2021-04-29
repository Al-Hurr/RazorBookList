using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IWebHostEnvironment _appEnvironment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public bool IsStoresExists { get; set; }

        [BindProperty]
        public BookCreateModel Book { get; set; }

        [BindProperty]
        public int[] StoreIds { get; set; }

        public SelectList Authors { get; set; }

        public SelectList Stores { get; set; }

        public async Task OnGet()
        {
            var authors = await _context.Authors.ToListAsync();

            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            var stores = await _context.Store.ToListAsync();

            Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

            IsStoresExists = stores.Any();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Name = Book.Name,
                    Author = Book.AuthorId == 0 ? null : await _context.Authors.FindAsync(Book.AuthorId),
                    ISBN = Book.ISBN,
                    Price = Book.Price,
                    Description = Book.Description
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

                await _context.AddAsync(book);
                await _context.SaveChangesAsync();

                // Передаем Id сущностей Store и создаем многосвязную таблицу Books - Stores (RelStoreBook)
                if (StoreIds.Length != 0)
                {
                    var createdBook = await _context.Books
                        .FirstOrDefaultAsync(x => x.Name == book.Name && x.Author == book.Author && x.ISBN == book.ISBN);

                    for (int i = 0; i < StoreIds.Length; i++)
                    {
                        var bookStore = new RelStoreBook()
                        {
                            Book = createdBook,
                            Store = await _context.Store.FindAsync(StoreIds[i])
                        };

                        await _context.AddAsync(bookStore);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
