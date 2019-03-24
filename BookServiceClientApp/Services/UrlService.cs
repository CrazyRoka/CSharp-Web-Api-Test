using System;
using System.Collections.Generic;
using System.Text;

namespace BookServiceClientApp.Services
{
    public class UrlService
    {
        public string BaseAddress => "http://localhost/";
        public string BooksApi => "api/BookChapters/";
    }
}
