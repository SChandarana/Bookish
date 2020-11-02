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
        public IEnumerable<string> ErrorMessage { get; }

        public AddBookViewModel(string isbn, string title, string authors, int copies, bool isbnError)
        {
            Isbn = isbn;
            Title = title;
            Authors = authors;
            Copies = copies;
            ErrorMessage = CreateErrorMessage(isbnError);

        }

        private IEnumerable<string> CreateErrorMessage(bool isbnError)
        {
            var errorMessage = new List<string>();

            if (Copies < 1)
            {
                errorMessage.Add("Must add at least one copy");
            }

            if (isbnError)
            {
                errorMessage.Add("That ISBN has already been used");
            }

            return errorMessage;
        }
    }
}
