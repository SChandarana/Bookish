using System.Collections.Generic;

namespace Bookish.DataAccess
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks();
    }
}