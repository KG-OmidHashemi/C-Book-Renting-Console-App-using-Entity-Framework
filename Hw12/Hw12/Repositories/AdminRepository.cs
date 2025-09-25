using Hw12.DataAccess;
using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        Hw12DbContext _db = new Hw12DbContext();

        public void Add(Admin admin)
        {
            _db.Admins.Add(admin);
            _db.SaveChanges();
        }

        public Admin? GetByUsername(string username)
        {
            return _db.Admins
                .FirstOrDefault(u => u.UserName == username);
        }
    }
}
