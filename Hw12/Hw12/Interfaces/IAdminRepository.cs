using Hw12.Enteties;
using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface IAdminRepository
    {
        public void Add(Admin admin);
        public Admin? GetByUsername(string username);
    }
}
