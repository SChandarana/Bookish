using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class BookViewModel
    {
        public Book Book { get; }

        public BookViewModel(Book book)
        {
            Book = book;
        }
    }
}
