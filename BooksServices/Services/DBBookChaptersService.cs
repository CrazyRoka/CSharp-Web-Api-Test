using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksServices.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksServices.Services
{
    public class DBBookChaptersService : IBookChaptersService
    {
        private readonly BooksContext _context;
        public DBBookChaptersService(BooksContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BookChapter chapter)
        {
            await _context.BookChapters.AddAsync(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            await _context.BookChapters.AddRangeAsync(chapters);
            await _context.SaveChangesAsync();
        }

        public Task<BookChapter> FindAsync(Guid id) =>
            _context.BookChapters.FindAsync(id);

        public async Task<IEnumerable<BookChapter>> GetAllAsync() =>
            await _context.BookChapters.ToListAsync();

        public async Task<BookChapter> RemoveAsync(Guid id)
        {
            BookChapter chapter = await _context.BookChapters.SingleOrDefaultAsync(c => c.Id == id);
            if (chapter == null) return null;

            _context.BookChapters.Remove(chapter);
            await _context.SaveChangesAsync();
            return chapter;
        }

        public async Task UpdateAsync(BookChapter chapter)
        {
            _context.BookChapters.Update(chapter);
            await _context.SaveChangesAsync();
        }
    }
}
