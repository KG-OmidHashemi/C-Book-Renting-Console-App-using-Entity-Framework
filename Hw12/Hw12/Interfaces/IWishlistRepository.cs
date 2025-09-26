using Hw12.Enteties;
using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interfaces
{
    public interface IWishlistRepository
    {
        public Wishlist? FindWishlistWithWishListId(int userId, int wishlistId);


        public Wishlist? FindWishlistWithUserId(User user, int bookId);

        public bool AddWishlist(Wishlist wishlist);


        public List<Wishlist> ShowUserWishlist(User user);


        public bool RemoveWishlist(User user, int wishlistId);


        public int GetWishlistCountForBook(int bookId);



    }
}
