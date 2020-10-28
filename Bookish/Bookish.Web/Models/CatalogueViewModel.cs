using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueViewModel
    {
        public IEnumerable<Book> Books { get; }

        public CatalogueViewModel(IEnumerable<Book> books)
        {
            Books = books;
        }
    }
}
