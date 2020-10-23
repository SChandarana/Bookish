using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess
{
    public class Library
    {
        private readonly IDbConnection database = new SqlConnection("Server=localhost;Database=LibraryDB;Trusted_Connection=True;");
        public IEnumerable<Book> GetBooks()
        {
            return database.Query<Book>("SELECT * FROM Books");
        }
    }
}