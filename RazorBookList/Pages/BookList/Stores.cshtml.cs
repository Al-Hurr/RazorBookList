using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages.BookList
{
    public class StoresModel : PageModel
    {
        private readonly BookService _bookService;

        public StoresModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public List<RelStoreBook> BookStores { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            BookStores = await _bookService.GetBookStoresAsync(id.Value);

            return Page();
        }

        public void OnPost()
        {
            
        }
    }
}
