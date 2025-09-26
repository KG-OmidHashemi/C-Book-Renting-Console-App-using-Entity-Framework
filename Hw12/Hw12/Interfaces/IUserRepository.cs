using Hw12.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User? GetByUsername(string username);
        void Update(User user);
        public List<User> GetAllUsers();
    }
}
