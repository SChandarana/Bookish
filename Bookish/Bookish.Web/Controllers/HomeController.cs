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

        public IActionResult Catalogue(string search = "")
        {
            var books = libraryService.GetBooks(search);
            return View(new CatalogueViewModel(books, search));
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
