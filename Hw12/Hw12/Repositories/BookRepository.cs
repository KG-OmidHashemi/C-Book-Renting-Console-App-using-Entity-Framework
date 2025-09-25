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
    public class BookRepository : IBookRepository
    {
        Hw12DbContext _db = new Hw12DbContext();
        public List<Book> GetAllBooks()
        {
            return _db.Books
                .ToList();
        }

        public List<Book> GetByCategoryId(int categoryId)
        {
            return _db.Books
                .Where(b => b.CategoryId == categoryId)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _db.Books
                .FirstOrDefault(b => b.Id == id);
        }

        public Book? GetByTitleAndCategory(string title, int categoryId)
        {
            return _db.Books
                .FirstOrDefault(b => b.Title == title && b.CategoryId == categoryId);
        }

        public void Add(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
    }
}
