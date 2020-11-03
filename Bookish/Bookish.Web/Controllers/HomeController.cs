using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bookish.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Web.Models;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ILibraryService libraryService;

        public HomeController(ILogger<HomeController> logger, ILibraryService libraryService)
        {
            this.logger = logger;
            this.libraryService = libraryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Catalogue(string searchTerm = "", int pageNumber = 1)
        {
            var books = libraryService.GetBooks(searchTerm);
            return View(new CatalogueViewModel(books, searchTerm, pageNumber));
        }

        public IActionResult Loans()
        {
            return View();
        }

        public IActionResult Book(string isbn)
        {
            var book = libraryService.GetCopies(isbn);
            if (book == null)
            {
                return RedirectToAction("Error");
            }
            return View(new BookViewModel(book));
        }

        public IActionResult AddBook(string isbn = "", string title = "", string authors = "", int copies = 1)
        {
            return View(new AddBookViewModel(isbn, title, authors, copies, libraryService.BookExists(isbn)));
        }

        [HttpPost]
        public IActionResult AddBookPost(string title, string authors, string isbn, int copies)
        {
            var isbnError = libraryService.BookExists(isbn);
            var copyError = copies < 1;
            if (isbnError || copyError)
            {
                return RedirectToAction("AddBook", new { isbn, title, authors, copies });
            }
            
            libraryService.AddBook(isbn, title, authors, copies);
            return RedirectToAction("Catalogue");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
