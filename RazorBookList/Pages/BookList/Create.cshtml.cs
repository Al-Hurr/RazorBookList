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
using RazorBookList.Services;

namespace RazorBookList.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly StoreService _storeService;

        public CreateModel(BookService bookService, AuthorService authorService, StoreService storeService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _storeService = storeService;
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
            var authors = await _authorService.GetAllAsync();

            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            var stores = await _storeService.GetAllAsync();

            Stores = new SelectList(stores, nameof(Store.Id), nameof(Store.Name));

            IsStoresExists = stores.Any();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateAsync(Book, StoreIds);

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
