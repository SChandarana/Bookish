using System;
using System.Linq;
using Bookish.DataAccess;
namespace Bookish.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var library = new LibraryService();
            var books = library.GetBooks()
                .Select(book => $"{book.isbn} - TITLE: {book.title} - AUTHORS: {book.authors}");
            Console.WriteLine(string.Join("\n", books));
        }
    }
}
