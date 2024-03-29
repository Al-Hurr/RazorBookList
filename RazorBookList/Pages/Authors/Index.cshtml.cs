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

namespace RazorAuthors.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly AuthorService _authorService;

        public IndexModel(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public List<Author> Authors { get; set; }

        [ModelBinder]
        public string FirstName { get; set; }

        [ModelBinder]
        public string LastName { get; set; }

        [ModelBinder]
        public string City { get; set; }

        public async Task OnGet()
        {
            Authors = await _authorService.GetAllAsync();

            if (!string.IsNullOrEmpty(FirstName))
            {
                Authors = Authors.Where(x => x.FirstName.ToLower().Contains(FirstName.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                Authors = Authors.Where(x => x.LastName.ToLower().Contains(LastName.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(City))
            {
                Authors = Authors.Where(x => x.City.ToLower().Contains(City.Trim().ToLower())).ToList();
            }
        }
    }
}
