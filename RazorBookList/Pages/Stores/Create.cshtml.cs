using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages.Stores
{
    public class CreateModel : PageModel
    {
        private readonly StoreService _storeService;

        public CreateModel(StoreService bookService)
        {
            _storeService = bookService;
        }

        [BindProperty]
        public Store Store { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _storeService.CreateAsync(Store);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
