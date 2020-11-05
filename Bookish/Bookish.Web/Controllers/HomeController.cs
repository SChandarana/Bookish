using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Bookish.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookish.Web.Models;
using Microsoft.AspNetCore.Hosting;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ILibraryService libraryService;
        private readonly IWebHostEnvironment webEnvironment;
        private readonly IBarcodeService barcodeService;

        public HomeController(ILogger<HomeController> logger, ILibraryService libraryService, IWebHostEnvironment webEnvironment, IBarcodeService barcodeService)
        {
            this.logger = logger;
            this.libraryService = libraryService;
            this.webEnvironment = webEnvironment;
            this.barcodeService = barcodeService;
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
            if (isbnError)
            {
                return RedirectToAction("AddBook", new { isbn, title, authors, copies });
            }
            
            libraryService.AddBook(isbn, title, authors, copies);
            return RedirectToAction("AddedBook", new { isbn });
        }

        public IActionResult AddedBook(string isbn)
        {
            var imageFolder = Path.Combine(webEnvironment.WebRootPath, "images", "barcodes");
            var newCopies = barcodeService.GetNewCopiesWithBarcodes(isbn, imageFolder);
            return View(new AddedBookViewModel(newCopies));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
