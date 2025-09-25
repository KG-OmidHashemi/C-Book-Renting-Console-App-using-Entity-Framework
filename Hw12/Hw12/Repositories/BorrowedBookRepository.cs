using Hw12.DataAccess;
using Hw12.Entities;
using Hw12.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        Hw12DbContext _db = new Hw12DbContext();
        public void Add(BorrowedBook borrowedBook)
        {
            _db.BorrowedBooks.Add(borrowedBook);
            _db.SaveChanges();
        }

        public List<BorrowedBook> GetBorrowedBooks(int userId)
        {
            return _db.BorrowedBooks
                .Where(bb => bb.UserId == userId)
                .Include(bb => bb.Book)
                .ToList();
        }
    }
}
