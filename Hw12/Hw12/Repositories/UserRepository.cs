using Hw12.DataAccess;
using Hw12.Enteties;
using Hw12.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Repositories
{
    public class UserRepository : IUserRepository
    {
        Hw12DbContext _db = new Hw12DbContext();

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public User? GetByUsername(string username)
        {
            return _db.Users
                .FirstOrDefault(u => u.UserName == username);
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public void Update(User user)
        {
            _db.Users.Update(user); 
            _db.SaveChanges();
        }
    }
}
