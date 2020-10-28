#nullable enable
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Bookish.DataAccess
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks(string search);

        Book? GetCopies(string isbn);
    }

    public class LibraryService : ILibraryService
    {
        private readonly IDbConnection databaseConnection;

        public LibraryService(IDbConnection connection)
        {
            databaseConnection = connection;
        }

        public IEnumerable<Book> GetBooks(string search)
        {
            return databaseConnection.Query<Book>($"SELECT * FROM Books WHERE Books.title LIKE '%{search}%' OR Books.authors LIKE '%{search}%'");
        }

        public Book? GetCopies(string isbn)
        {
            var query =
                "SELECT LibraryBooks.isbn, Books.title, Books.authors, LibraryBooks.bookId, AspNetUsers.UserName, Loans.dueDate " +
                "FROM LibraryBooks " +
                "JOIN Books ON LibraryBooks.isbn=Books.isbn " +
                "LEFT JOIN Loans ON LibraryBooks.bookId=Loans.bookId " +
                "LEFT JOIN AspNetUsers ON AspNetUsers.Id=Loans.userId " +
                $"WHERE LibraryBooks.isbn='{isbn}';";

            Book? outBook = null;

            var bookWithCopies = databaseConnection.Query<Book, BookCopy, Book>(
                    query,
                    (book, bookCopy) =>
                    {
                        outBook ??= book;
                        outBook.bookCopies.Add(bookCopy);
                        return outBook;
                    },
                    splitOn: "bookId")
                .FirstOrDefault();
                
            return bookWithCopies;
        }
    }
}