using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly AuthorService _authorService;

        public CreateModel(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public Author Author { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAsync(Author);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
