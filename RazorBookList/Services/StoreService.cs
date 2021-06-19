using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Services
{
    public class StoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Store>> GetAllAsync()
        {
            return await _context.Store.ToListAsync();
        }
    }
}
