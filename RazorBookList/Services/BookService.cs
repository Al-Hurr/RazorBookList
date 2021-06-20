using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazorBookList.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthorService _authorService;

        public BookService(ApplicationDbContext context, AuthorService authorService)
        {
            _context = context;
            _authorService = authorService;
        }

        public async Task<Book> GetAsync(int id, bool noTracking = false)
        {
            if (noTracking)
            {
                return await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                return await _context.Books
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<List<RelStoreBook>> GetBookStoresAsync(int id)
        {
            return await _context.RelStoreBook
                .Include(x => x.Store)
                .Include(x => x.Book)
                .Where(x => x.Book.Id == id)
                .ToListAsync();
        }

        public byte[] ImageToByte(IFormFile image)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)image.Length);
            }
            return imageData;
        }

        public async Task CreateOrUpdateBookStores(Book book, int[] storesId)
        {
            var bookStores = await GetBookStoresAsync(book.Id);

            // Если есть старые записи, то сначало их удаляем, чтобы не было повторений.
            if (bookStores != null && bookStores.Any())
            {
                _context.RelStoreBook.RemoveRange(bookStores);
            }

            for (int i = 0; i < storesId.Length; i++)
            {
                var BookStore = new RelStoreBook()
                {
                    Book = book,
                    Store = await _context.Store.FindAsync(storesId[i])
                };

                await _context.AddAsync(BookStore);
            }
        }

        public async Task UpdateAsync(BookEditModel bookEditModel, int[] storesId)
        {
            var book = new Book()
            {
                Id = bookEditModel.Id,
                Name = bookEditModel.Name,
                Author = await _authorService.GetAsync(bookEditModel.AuthorId),
                ISBN = bookEditModel.ISBN,
                Price = bookEditModel.Price,
                Description = bookEditModel.Description
            };

            // Если выбрана новая картинка, то меняем картинку
            if (bookEditModel.Image != null)
            {
                if (bookEditModel.Image.Length > 0)
                {
                    book.Cover = ImageToByte(bookEditModel.Image);
                }
            }
            //Иначе оставляем старую (сначало вытаскиваю ее из базы, чтобы сохранился, т.к. он не биндится)
            else
            {
                var bookFromDb = await GetAsync(book.Id, true);
                book.Cover = bookFromDb.Cover;
            }

            _context.Update(book);

            if (storesId.Length != 0)
            {
                await CreateOrUpdateBookStores(book, storesId);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetAsync(id);

            // Если в таблице RelStoreBook есть связанные сущности с таблицей Book, то сначало удаляем их.
            if (IsExistStores(book.Id))
            {
                var bookStores = _context.RelStoreBook.Where(x => x.Book.Id == book.Id);
                _context.RelStoreBook.RemoveRange(bookStores);
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public bool IsExistStores(int id)
        {
            return _context.RelStoreBook.Any(x => x.Book.Id == id);
        }

        public async Task CreateAsync(BookCreateModel bookCreateModel, int[] storesId)
        {
            var book = new Book()
            {
                Name = bookCreateModel.Name,
                Author = bookCreateModel.AuthorId == 0 ? null : await _authorService.GetAsync(bookCreateModel.AuthorId),
                ISBN = bookCreateModel.ISBN,
                Price = bookCreateModel.Price,
                Description = bookCreateModel.Description
            };

            if (bookCreateModel.Image != null)
            {
                if (bookCreateModel.Image.Length > 0)
                {
                    book.Cover = ImageToByte(bookCreateModel.Image);
                }
            }

            await _context.AddAsync(book);
            //Сначало сохраняем, чтобы записать сущность в бд
            await _context.SaveChangesAsync();

            // Передаем Id сущностей Store и создаем многосвязную таблицу Books - Stores (RelStoreBook)
            if (storesId.Length != 0)
            {
                var createdBook = await _context.Books
                    .FirstOrDefaultAsync(x => x.Name == book.Name && x.Author == book.Author && x.ISBN == book.ISBN);

                await CreateOrUpdateBookStores(createdBook, storesId);
                await _context.SaveChangesAsync();
            }
        }
    }
}
