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
using RazorBookList.Services;

namespace RazorBookList.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly StoreService _storeService;

        public EditModel(BookService bookService, AuthorService authorService, StoreService storeService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _storeService = storeService;
        }
        public bool IsStoresExists { get; set; }

        [BindProperty]
        public BookEditModel Book { get; set; }
        = new BookEditModel();

        [BindProperty]
        public int[] StoresId { get; set; }

        public SelectList Authors { get; set; }
        public SelectList Stores { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var authors = await _authorService.GetAllAsync();
            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            var stores = await _storeService.GetAllAsync();

            IsStoresExists = stores.Any();

            Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

            var bookStores = await _bookService.GetBookStoresAsync(id.Value);
            StoresId = bookStores.Select(x => x.Store.Id).ToArray();

            var book = await _bookService.GetAsync(id.Value);

            Book.Id = book.Id;
            Book.Name = book.Name;
            Book.AuthorId = book.Author?.Id ?? default;
            Book.ISBN = book.ISBN;
            Book.Price = book.Price;
            Book.Description = book.Description;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateAsync(Book, StoresId);

                return RedirectToPage("Index");
            }
            return NotFound("Model is not valid");
        }
    }
}
