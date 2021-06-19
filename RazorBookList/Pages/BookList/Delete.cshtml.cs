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
    public class DeleteModel : PageModel
    {
        private readonly BookService _bookService;

        public DeleteModel(BookService bookService)
        {
            _bookService = bookService;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Book = await _bookService.GetAsync(id.Value);

            if (Book == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            await _bookService.DeleteAsync(id.Value);

            return RedirectToPage("Index");
        }
    }
}
