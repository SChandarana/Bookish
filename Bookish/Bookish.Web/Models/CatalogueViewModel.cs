using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueViewModel
    {
        public IEnumerable<Book> Books { get; }
        public string SearchTerm { get; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        private const int ItemsPerPage = 5;

        public CatalogueViewModel(IEnumerable<Book> books, string searchTerm, int pageNumber)
        {
            var allBooks = books.ToList();
            SearchTerm = searchTerm;
            TotalPages = (int)Math.Ceiling((double) allBooks.Count() / ItemsPerPage);
            PageNumber = GetValidatedPageNumber(pageNumber);
            Books = allBooks.Skip(ItemsPerPage * (PageNumber - 1)).Take(ItemsPerPage);
        }

        public string GetDisabledPrevious()
        {
            return PageNumber == 1 ? "disabled" : "";
        }

        public string GetDisabledNext()
        {
            return PageNumber == TotalPages ? "disabled" : "";
        }

        public string GetActiveClass(int pageNumber)
        {
            return pageNumber == PageNumber ? "active" : "";
        }

        private int GetValidatedPageNumber(int pageNumber)
        {
            if (pageNumber < 1)
            {
                return 1;
            }

            if (pageNumber > TotalPages)
            {
                return TotalPages;
            }

            return pageNumber;
        }
    }
}
