using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.BookList
{
    public class StoresModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StoresModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<RelStoreBook> BookStores { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            BookStores = await _context.RelStoreBook
                .Include(x => x.Store)
                .Include(x => x.Book)
                .Where(x => x.Book.Id == id)
                .ToListAsync();

            return Page();
        }

        public void OnPost()
        {
            
        }
    }
}
