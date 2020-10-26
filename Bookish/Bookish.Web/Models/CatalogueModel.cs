using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueModel
    {
        public IEnumerable<Book> Books { get; private set; }

        public CatalogueModel(IEnumerable<Book> books)
        {
            Books = books;
        }
    }
}
