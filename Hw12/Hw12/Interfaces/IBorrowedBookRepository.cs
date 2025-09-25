using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface IBorrowedBookRepository
    {
        public void Add(BorrowedBook borrowedBook);
        public List<BorrowedBook> GetBorrowedBooks(int userId);

    }
}
