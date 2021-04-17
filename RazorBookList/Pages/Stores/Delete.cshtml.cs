using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Pages.Stores
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Store Store { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Store = await _context.Store
                .FindAsync(id.Value);

            if (Store == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var store = await _context.Store.FindAsync(id.Value);

            if (store == null)
                return NotFound();

            if(await _context.RelStoreBook.AnyAsync(x => x.Store.Id == store.Id))
            {
                var storeBooks = _context.RelStoreBook.Where(x => x.Store.Id == store.Id);
                _context.RelStoreBook.RemoveRange(storeBooks);
            }

            _context.Store.Remove(store);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
