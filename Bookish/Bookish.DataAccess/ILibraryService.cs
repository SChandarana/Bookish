using System.Collections.Generic;

namespace Bookish.DataAccess
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks();

        Book GetBook(string isbn);

        Book GetCopies(string isbn);
    }
}