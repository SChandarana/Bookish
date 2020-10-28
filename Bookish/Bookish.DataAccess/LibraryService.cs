using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    }
}