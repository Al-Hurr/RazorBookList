using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages
{
    public class Book_PageModel : PageModel
    {
        private readonly BookService _bookService;

        public Book_PageModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public Book Book { get; set; }

        public List<RelStoreBook> BookStores { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            Book = await _bookService.GetAsync(id.Value);
            BookStores = await _bookService.GetBookStoresAsync(id.Value);

            return Page();
        }
    }
}
