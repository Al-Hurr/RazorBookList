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
    public class IndexModel : PageModel
    {
        private readonly BookService _bookService;

        public IndexModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public List<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _bookService.GetAllAsync();
        }
    }
}
