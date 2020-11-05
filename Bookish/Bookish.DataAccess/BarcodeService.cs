using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using BarcodeLib;

namespace Bookish.DataAccess
{
    public interface IBarcodeService
    {
        IEnumerable<NewCopy> GetNewCopiesWithBarcodes(string isbn, string imageFolder);
    }
    
    public class BarcodeService : IBarcodeService
    {
        private readonly ILibraryService libraryService;
        public BarcodeService(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public IEnumerable<NewCopy> GetNewCopiesWithBarcodes(string isbn, string imageFolder)
        {
            Clean(imageFolder);
            var newCopies = libraryService.GetNewCopies(isbn).ToList();
            foreach (var copy in newCopies)
            {
                CreateBarcodeImage(copy.bookId, imageFolder);
            }

            return newCopies;
        }

        private static void CreateBarcodeImage(int bookId, string imageFolder)
        {
            var path = Path.Combine(imageFolder, $"barcode{bookId}.png");
            if (File.Exists(path)) return; // To avoid errors when overwriting files in use
            var barcode = new Barcode();
            var image = barcode.Encode(TYPE.CODE39, bookId.ToString(), Color.Black, Color.White, 350, 200);
            image.Save(new FileStream(path, FileMode.Create), ImageFormat.Png);
        }

        private static void Clean(string folder)
        {
            try
            {
                var images = Directory.GetFiles(folder);
                foreach (var image in images)
                {
                    File.Delete(image);
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Couldn't delete files in image folder");
            }
            
        }
    }
}
