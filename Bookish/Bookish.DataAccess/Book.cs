using System;
using System.Collections.Generic;

namespace Bookish.DataAccess
{
    public class Book
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public List<BookCopy> bookCopies { get; set; }
    }
}
