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
    public class CategoryRepository : ICategoryRepository
    {
        Hw12DbContext _db = new Hw12DbContext();
        public List<Category> GetAllCategories()
        {
            return _db.Categories
                .ToList();
        }

        public Category? GetById(int id)
        {
            return _db.Categories
                .Include(c => c.Books)
                .FirstOrDefault(c => c.Id == id);
        }
        public Category? GetByName(string categoryName)
        {
            return _db.Categories
                .FirstOrDefault(c => c.Name == categoryName);
        }

        public void Add(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public bool Any() => _db.Categories.Any();
    }
}
