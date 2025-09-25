using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
