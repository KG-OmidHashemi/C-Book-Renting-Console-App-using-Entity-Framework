using Hw12.DataAccess;
using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Repositories
{
    public class WishlistRepository:IWishlistRepository
    {
        Hw12DbContext _db = new Hw12DbContext();


        public Wishlist? FindWishlistWithWishListId(int userId, int wishlistId)
        {
            var wishlist = _db.wishlists.FirstOrDefault(w => w.Id == wishlistId&&userId==w.UserId);
            if (wishlist == null)
                return null;
            return wishlist;
        }

        public Wishlist? FindWishlistWithUserId(User user, int bookId)
        {
            return _db.wishlists
                      .FirstOrDefault(w => w.UserId == user.Id && w.BookId == bookId);
        }
        public bool AddWishlist(Wishlist wishlist)
        { 
            _db.Add(wishlist);
            _db.SaveChanges();
            return true;
        }

        public List<Wishlist> ShowUserWishlist(User user)
        {
            return _db.wishlists
                .Where(u => u.Id == user.Id)
                .ToList();
        }

        public bool RemoveWishlist(User user,int wishlistId)
        {

            var wishlist = FindWishlistWithWishListId(user.Id,wishlistId);
            if (wishlist == null)
                return false;
            _db.wishlists.Remove(wishlist);
            _db.SaveChanges();
            return true;
        }

        public int GetWishlistCountForBook(int bookId)
        {
            return _db.wishlists.Count(w => w.BookId == bookId);
        }

    }
}
