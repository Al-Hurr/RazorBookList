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

namespace RazorBookList.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        [ModelBinder]
        public string SearchString { get; set; }

        public SelectList Authors { get; set; }

        [ModelBinder]
        public int? AuthorId { get; set; }

        [ModelBinder]
        public int? PriceTo { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Books = await _context.Books
                .Include(x => x.Author)
                .ToListAsync();

            var authors = await _context.Authors.ToListAsync();

            Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.LastName));

            if (!String.IsNullOrEmpty(SearchString))
            {
                Books = await _context.Books.Where(x => x.Name.ToLower().Contains(SearchString.Trim().ToLower())).ToListAsync();
            }

            if (AuthorId.HasValue)
            {
                Books = await _context.Books.Where(x => x.Author.Id == AuthorId).ToListAsync();
            }

            if (PriceTo.HasValue)
            {
                Books = await _context.Books.Where(x => x.Price <= PriceTo).ToListAsync();
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostDelete(int? id)
        //{
        //    if (!id.HasValue)
        //        return NotFound();

        //    var book = await _context.Books.FindAsync(id.Value);

        //    if(book == null)
        //        return NotFound();

        //    _context.Books.Remove(book);
        //    _context.SaveChanges();

        //    return RedirectToPage();
        //}
    }
}
