using System;
using System.Collections.Generic;
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
    public class IndexModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public IndexModel(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public List<Book> Books { get; set; }

        [ModelBinder]
        public string SearchString { get; set; }

        public SelectList Authors { get; set; }

        [ModelBinder]
        public int? AuthorId { get; set; }

        [ModelBinder]
        public int? PriceTo { get; set; }

        public async Task OnGet()
        {
            Books = await _bookService.GetAllAsync();

            var authors = await _authorService.GetAllAsync();

            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            if (!String.IsNullOrEmpty(SearchString))
            {
                Books = Books.Where(x => x.Name.ToLower().Contains(SearchString.Trim().ToLower())).ToList();
            }

            if (AuthorId.HasValue && AuthorId != 0)
            {
                Books = Books.Where(x => x.Author.Id == AuthorId).ToList();
            }

            if (PriceTo.HasValue)
            {
                Books = Books.Where(x => x.Price <= PriceTo).ToList();
            }
        }
    }
}
