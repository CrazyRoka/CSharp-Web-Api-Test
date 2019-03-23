using BooksServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksServices.Services
{
    public interface IBookChaptersService
    {
        void Add(BookChapter bookChapter);
        void AddRange(IEnumerable<BookChapter> chapters);
        IEnumerable<BookChapter> GetAll();
        BookChapter Find(Guid id);
        BookChapter Remove(Guid id);
        void Update(BookChapter bookChapter);
    }
}
