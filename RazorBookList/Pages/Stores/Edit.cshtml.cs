using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages.Stores
{
    public class EditModel : PageModel
    {
        private readonly StoreService _storeService;

        public EditModel(StoreService bookService)
        {
            _storeService = bookService;
        }

        [BindProperty]
        public Store Store { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Store = await _storeService.GetAsync(id.Value);

            if (Store == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _storeService.UpdateAsync(Store);
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
