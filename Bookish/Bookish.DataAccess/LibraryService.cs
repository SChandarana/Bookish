using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess
{
    public class LibraryService : ILibraryService
    {
        private readonly IDbConnection databaseConnection = new SqlConnection("Server=localhost;Database=LibraryDB;Trusted_Connection=True;");

        public IEnumerable<Book> GetBooks()
        {
            return databaseConnection.Query<Book>("SELECT * FROM Books");
        }
    }
}