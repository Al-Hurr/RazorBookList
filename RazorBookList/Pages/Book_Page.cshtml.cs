using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorBookList.Model;

namespace RazorBookList.Pages
{
    public class Book_PageModel : PageModel
    {
        private readonly ILogger<Book_PageModel> _logger;
        private readonly ApplicationDbContext _context;

        public Book_PageModel(ILogger<Book_PageModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            Book = await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);

            return Page();
        }
    }
}
