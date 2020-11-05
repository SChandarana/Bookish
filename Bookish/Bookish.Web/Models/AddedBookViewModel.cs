using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class AddedBookViewModel
    {
        public IEnumerable<NewCopy> NewCopies { get; }

        public AddedBookViewModel(IEnumerable<NewCopy> newCopies)
        {
            NewCopies = newCopies;
        }
    }
}
