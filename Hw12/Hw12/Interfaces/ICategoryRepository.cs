using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category? GetById(int id);
        public Category? GetByName(string categoryName);
        public void Add(Category category);
        public bool Any();
    }
}
