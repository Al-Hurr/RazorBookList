using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorAuthors.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Store> Stores { get; set; }

        [ModelBinder]
        public string Name { get; set; }

        [ModelBinder]
        public string Adress { get; set; }

        [ModelBinder]
        public string WorkingTime { get; set; }

        public async Task OnGet()
        {
            Stores = await _context.Store.ToListAsync();

            if (!string.IsNullOrEmpty(Name))
            {
                Stores = await _context.Store.Where(x => x.Name.ToLower().Contains(Name.Trim().ToLower())).ToListAsync();
            }

            if (!string.IsNullOrEmpty(Adress))
            {
                Stores = await _context.Store.Where(x => x.Adress.ToLower().Contains(Adress.Trim().ToLower())).ToListAsync();
            }

            if (!string.IsNullOrEmpty(WorkingTime))
            {
                Stores = await _context.Store.Where(x => x.WorkingTime.ToLower().Contains(WorkingTime.Trim().ToLower())).ToListAsync();
            }
        }
    }
}
