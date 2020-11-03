#nullable enable
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Bookish.DataAccess
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks(string searchTerm);

        Book? GetCopies(string isbn);

        void AddBook(string isbn, string title, string authors, int copies);

        bool BookExists(string isbn);
    }

    public class LibraryService : ILibraryService
    {
        private readonly IDbConnection databaseConnection;

        public LibraryService(IDbConnection connection)
        {
            databaseConnection = connection;
        }

        public IEnumerable<Book> GetBooks(string searchTerm)
        {
            var sql = "SELECT * FROM Books WHERE Books.title LIKE @searchString OR Books.authors LIKE @searchString ORDER BY Books.title ASC;";
            return databaseConnection.Query<Book>(sql, new { searchString = $"%{searchTerm}%" });
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

        public void AddBook(string isbn, string title, string authors, int copies)
        {
            var sql = @"INSERT INTO Books(isbn, title, authors)
                        VALUES(@isbn, @title, @authors)
                            
                        DECLARE @i INT = 0;

                        WHILE @i < @copies
                        BEGIN
                            INSERT INTO LibraryBooks(isbn) 
                            VALUES (@isbn)
                            SET @i = @i + 1;
                        END;";

            databaseConnection.Execute(sql, new
            {
                isbn,
                title,
                authors,
                copies
            });
        }

        public bool BookExists(string isbn)
        {
            var sql = "SELECT * FROM Books WHERE Books.isbn = @isbn";
            return databaseConnection.Query<Book>(sql, new { isbn }).Any();
        }
    }
}