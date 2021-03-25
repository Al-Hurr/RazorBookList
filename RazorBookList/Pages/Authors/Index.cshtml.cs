using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorAuthors.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Author> Authors { get; set; }

        public async Task OnGet()
        {
            Authors = await _context.Authors.ToListAsync();
        }

        public bool IsRelated(int id)
        {
            return  _context.Books.Any(x => x.Author.Id == id);
        }
    }
}
