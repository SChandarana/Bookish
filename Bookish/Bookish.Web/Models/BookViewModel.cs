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

        public bool IsAvailable(BookCopy copy)
        {
            return copy.username != null;
        }

        public string GetDisabledClass(BookCopy copy)
        {
            return IsAvailable(copy)
                ? "disabled"
                : "";
        }

        public string OwnedBy(BookCopy copy)
        {
            return IsAvailable(copy) 
                ? $"UNAVAILABLE - borrowed by {copy.username} and due: {copy.dueDate:d}" 
                : "";
        }
    }
}
