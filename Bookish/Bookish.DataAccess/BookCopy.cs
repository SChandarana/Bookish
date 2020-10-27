using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookish.DataAccess
{
    public class BookCopy
    {
        public int bookId { get; set; }
        public string username { get; set; }
        public DateTime dueDate { get; set; }
    }
}
