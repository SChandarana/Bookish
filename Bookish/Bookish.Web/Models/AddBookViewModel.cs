using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookish.Web.Models
{
    public class AddBookViewModel
    {
        public string Isbn { get; }
        public string Title { get; }
        public string Authors { get; }
        public int Copies { get; }
        public string ErrorMessage { get; }

        public AddBookViewModel(string isbn, string title, string authors, int copies, bool isbnError)
        {
            Isbn = isbn;
            Title = title;
            Authors = authors;
            Copies = copies;
            ErrorMessage = isbnError ? "ISBN already used" : "";
        }
    }
}
