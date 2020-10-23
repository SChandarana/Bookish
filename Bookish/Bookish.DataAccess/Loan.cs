using System;

namespace Bookish.DataAccess
{
    class Loan
    {
        public int userId { get; set; }
        public int bookId { get; set; }
        public DateTime dueDate { get; set; }
    }
}
