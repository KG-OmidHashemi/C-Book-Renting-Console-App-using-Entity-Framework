using Hw12.Enteties;
using Hw12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Interface
{
    public interface IReviewRepository
    {
        public List<Review> GetAllReviews();
        public Review? GetUserReviewForBook(int userId, int bookId);
        public List<Review> GetAllUserReviews(int userId);
        public bool EditReview(Review review);
        public bool DeleteReview(Review review);
        public void Add(Review review);
    }
}
