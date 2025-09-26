using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Interface;
using Hw12.Interfaces;
using Hw12.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Services
{

    public class UserService
    {
        IUserRepository _userRepo = new UserRepository();
        ICategoryRepository _categoryRepo = new CategoryRepository();
        IBookRepository _bookRepo = new BookRepository();
        IBorrowedBookRepository _borrowRepo = new BorrowedBookRepository();
        IReviewRepository _reviewRepo = new ReviewRepository();
        IWishlistRepository _wishRepo = new WishlistRepository();
        public List<Category> GetCategories()
        {
            return _categoryRepo.GetAllCategories()
                .ToList();
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepo.GetAllBooks().ToList();
        }

        public List<Book> GetBooks(int categoryId)
        {
            var category = _categoryRepo.GetById(categoryId);
            if (category == null)
                throw new Exception("Category was not found!!!");

            var books = _bookRepo.GetAllBooks()
                .Where(b => b.CategoryId == categoryId)
                .ToList();

            if (books.Count == 0)
                throw new Exception("No books in this Category!!!");

            return books;
        }

        public Book GetBookById(int bookId)
        {
            var book = _bookRepo.GetById(bookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            return book;
        }

        public bool BorrowBook(User user, int bookId)
        {
            GetBookById(bookId);
            var borrowed = new BorrowedBook
            {
                BookId = bookId,
                UserId = user.Id,
                BorrowDate = DateTime.Now
            };

            _borrowRepo.Add(borrowed);
            return true;
        }

        public List<BorrowedBook> ShowBorrowedBooks(User user)
        {
            return _borrowRepo.GetBorrowedBooks(user.Id)
                .ToList();
        }

        public bool AddComment(User user, string comment, int bookId, int rating)
        {
            var review = new Review
            {
                Username = user.UserName,
                BookId = bookId,
                Rating = rating,
                UserId = user.Id,
                Comment = comment,
            };
            _reviewRepo.Add(review);
            return true;

        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepo.GetAllReviews();
        }

        public List<Review> GetUserReviews(User user)
        {
            return _reviewRepo.GetAllUserReviews(user.Id);
        }

        public bool EditReview(User user, int reviewId, string editedComment, int bookReviewID, int editedRating)
        {
            var review = _reviewRepo.GetUserReviewForBook(user.Id, bookReviewID);
            if (review == null)
                throw new Exception("Your review was not found with the given review Id");

            if (review.Id == reviewId)
            {
                review.Comment = editedComment;
                review.Rating = editedRating;
                review.IsConfirmed = false;
                _reviewRepo.EditReview(review);
                return true;
            }

            return false;

        }

        public bool DeleteReview(User user, int reviewId)
        {
            var selectedReview = _reviewRepo.GetUserReviewForBook(user.Id, reviewId);
            if (selectedReview != null)
                _reviewRepo.DeleteReview(selectedReview);
            return true;

        }

        public bool AddToWishList(User user, int bookId)
        {
            var book=_bookRepo.GetById(bookId);
            if (book == null)
                throw new Exception("No book exists with that Id");
            var review=_wishRepo.FindWishlistWithUserId(user,bookId);
            if (review != null)
                throw new Exception("This book is already in your wishlist");
            var wishlist = new Wishlist
            {
                BookId = bookId,
                CreatedAt = DateTime.Now,
                UserId = user.Id,
            };
            _wishRepo.AddWishlist(wishlist);
            return true;
        }

        public List<Wishlist> ShowWishlist(User user)
        {
            return _wishRepo.ShowUserWishlist(user);
        }

        public bool RemoveWishlist(User user, int wishlistId)
        {
            bool result = _wishRepo.RemoveWishlist(user,wishlistId);
            if (!result)
            {
                throw new Exception("Wishlist id is wrong!!!");
            }
            return result;
        }
        public int WishlistCount(int bookId)
        {
            return _wishRepo.GetWishlistCountForBook(bookId);
        }

        public bool ReturnBook(User user, int bookId)
        {
            var borrowedBook = _borrowRepo.GetBorrowedBooks(user.Id)
                                          .FirstOrDefault(bb => bb.BookId == bookId);

            if (borrowedBook == null)
                throw new Exception("You did not borrow this book.");

            var daysBorrowed = (DateTime.Now - borrowedBook.BorrowDate).Days;

            if (daysBorrowed > 7)
            {
                int overdueDays = daysBorrowed - 7;
                int penalty = overdueDays * 10000;
                user.PenaltyAmount += penalty;
                _userRepo.Update(user);
            }

            _borrowRepo.RemoveBorrowedBook(user,borrowedBook.Id);

            return true;
        }

        public int GetUserPenalty(User user)
        {
            return user.PenaltyAmount;
        }
    }
}
