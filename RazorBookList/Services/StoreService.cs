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

        public async Task<Store> GetAsync(int id)
        {
            return await _context.Store.FindAsync(id);
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var store = await GetAsync(id);

            // Если в многосвязной таблице RelStoreBook есть записи, которые ссылаются на эту сущность, то сначало удаляем их.
            if (await _context.RelStoreBook.AnyAsync(x => x.Store.Id == store.Id))
            {
                var storeBooks = _context.RelStoreBook.Where(x => x.Store.Id == store.Id);
                _context.RelStoreBook.RemoveRange(storeBooks);
            }

            _context.Store.Remove(store);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Store store)
        {
            await _context.Store.AddAsync(store);
            await _context.SaveChangesAsync();
        }
    }
}
