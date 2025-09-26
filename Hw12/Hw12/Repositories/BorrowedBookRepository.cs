using Hw12.DataAccess;
using Hw12.Enteties;
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

        public BorrowedBook? FindBorrowedBookWithBorrowedBookId(int userId, int borrowedBookId)
        {
            var borrowedBook = _db.BorrowedBooks.FirstOrDefault(bb => bb.Id == borrowedBookId && userId == bb.UserId);
            if (borrowedBook == null)
                return null;
            return borrowedBook;
        }

        public bool RemoveBorrowedBook(User user, int borrowedBookId)
        {

            var borrowedBook = FindBorrowedBookWithBorrowedBookId(user.Id, borrowedBookId);
            if (borrowedBook == null)
                return false;
            _db.BorrowedBooks.Remove(borrowedBook);
            _db.SaveChanges();
            return true;
        }
    }
}
