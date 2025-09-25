using Hw12.DataAccess;
using Hw12.Enteties;
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
    public class ReviewRepository : IReviewRepository
    {
        Hw12DbContext _db = new Hw12DbContext();
        public List<Review> GetAllReviews()
        {
            return _db.Reviews
                .ToList();
        }

        public Review? GetUserReviewForBook(int userId, int bookId)
        {
            return _db.Reviews
                .FirstOrDefault(r => r.UserId == userId && r.BookId == bookId);
        }

        public List<Review> GetAllUserReviews(int userId)
        {
            return _db.Reviews
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public bool EditReview(Review review)
        {
            _db.Reviews.Update(review);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteReview(Review review)
        {
            _db.Reviews.Remove(review);
            _db.SaveChanges();
            return true;
        }

        public void Add(Review review)
        {
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }

    }
}
