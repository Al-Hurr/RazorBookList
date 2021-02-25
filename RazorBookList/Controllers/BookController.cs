using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorBookList.Model;

namespace RazorBookList.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(new { data = await _context.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return Json(new { success = false, message = "Error" });

            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id.Value);

            if (book == null)
                return Json(new { success = false, message = "Error" });

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Remove successful" });
        }
    }
}
