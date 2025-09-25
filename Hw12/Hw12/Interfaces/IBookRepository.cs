using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public List<Book> GetByCategoryId(int categoryId);
        public Book? GetById(int id);
        public Book? GetByTitleAndCategory(string title, int categoryId);
        public void Add(Book book);
    }
}
