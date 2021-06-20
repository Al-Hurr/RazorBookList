using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;
using RazorBookList.Services;

namespace RazorBookList.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly AuthorService _authorService;

        public DeleteModel(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public Author Author { get; set; }

        public bool IsAnyBookExist { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Author = await _authorService.GetAsync(id.Value);

            if (Author == null)
                return NotFound();

            // Проверка, ссылаются ли сущности Book на данную сущность
            IsAnyBookExist = await _authorService.IsAnyBookExist(id.Value);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            await _authorService.DeleteAsync(id.Value);

            return RedirectToPage("Index");
        }
    }
}
