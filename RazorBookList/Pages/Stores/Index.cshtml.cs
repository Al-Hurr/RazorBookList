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

namespace RazorAuthors.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly StoreService _storeService;

        public IndexModel(StoreService bookService)
        {
            _storeService = bookService;
        }

        public List<Store> Stores { get; set; }

        [ModelBinder]
        public string Name { get; set; }

        [ModelBinder]
        public string Adress { get; set; }

        [ModelBinder]
        public string WorkingTimeFrom { get; set; }

        [ModelBinder]
        public string WorkingTimeTo { get; set; }

        public async Task OnGet()
        {
            Stores = await _storeService.GetAllAsync();

            if (!string.IsNullOrEmpty(Name))
            {
                Stores = Stores.Where(x => x.Name.ToLower().Contains(Name.Trim().ToLower())).ToList();
            }   

            if (!string.IsNullOrEmpty(Adress))
            {
                Stores = Stores.Where(x => x.Adress.ToLower().Contains(Adress.Trim().ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(WorkingTimeFrom))
            {
                Stores = Stores.Where(x => x.WorkingTimeFrom.TimeOfDay >= Convert.ToDateTime(WorkingTimeFrom).TimeOfDay).ToList();
            }

            if (!string.IsNullOrEmpty(WorkingTimeTo))
            {
                Stores = Stores.Where(x => x.WorkingTimeTo.TimeOfDay <= Convert.ToDateTime(WorkingTimeTo).TimeOfDay).ToList();
            }
        }
    }
}
