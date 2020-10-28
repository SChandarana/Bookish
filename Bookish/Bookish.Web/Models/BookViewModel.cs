using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class BookViewModel
    {
        public Book Book { get; }

        public BookViewModel(Book book)
        {
            Book = book;
        }

        public string IsDisabled(BookCopy copy)
        {
            return copy.username != null ? "disabled" : "";
        }

        public string OwnedBy(BookCopy copy)
        {
            return copy.username != null 
                ? $"UNAVAILABLE - borrowed by {copy.username} and due: {copy.dueDate:d}" 
                : "";
        }
    }
}
