using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Enteties
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
        public int PenaltyAmount { get; set; }

    }
}
