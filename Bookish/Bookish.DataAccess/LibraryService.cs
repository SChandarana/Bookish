using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Bookish.DataAccess
{
    public class LibraryService : ILibraryService
    {
        private readonly IDbConnection databaseConnection;

        public LibraryService(IDbConnection connection)
        {
            databaseConnection = connection;
        }

        public IEnumerable<Book> GetBooks()
        {
            return databaseConnection.Query<Book>("SELECT * FROM Books");
        }

        public Book GetBook(string isbn)
        {
            return databaseConnection.QuerySingle<Book>($"SELECT * FROM Books WHERE isbn='{isbn}'");
        }

        public Book GetCopies(string isbn)
        {
            var query =
                "SELECT LibraryBooks.isbn, Books.title, Books.authors, LibraryBooks.bookId, Users.username, Loans.dueDate " +
                "FROM LibraryBooks " +
                "JOIN Books ON LibraryBooks.isbn=Books.isbn " +
                "LEFT JOIN Loans ON LibraryBooks.bookId=Loans.bookId " +
                "LEFT JOIN Users ON Users.userId=Loans.userId " +
                $"WHERE LibraryBooks.isbn='{isbn}';";

            var bookDictionary = new Dictionary<string, Book>();


            var bookWithCopies = databaseConnection.Query<Book, BookCopy, Book>(
                    query,
                    (book, bookCopy) =>
                    {
                        Book bookEntry;

                        if (!bookDictionary.TryGetValue(book.isbn, out bookEntry))
                        {
                            bookEntry = book;
                            bookEntry.bookCopies = new List<BookCopy>();
                            bookDictionary.Add(bookEntry.isbn, bookEntry);
                        }

                        bookEntry.bookCopies.Add(bookCopy);
                        return bookEntry;
                    },
                    splitOn: "bookId")
                .Distinct()
                .ToList();
            return bookWithCopies.Single();
        }
    }
}