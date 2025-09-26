using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Interface;
using Hw12.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Services
{
    public class AdminService
    {
        ICategoryRepository _categoryRepo = new CategoryRepository();
        IBookRepository _bookRepo = new BookRepository();
        IReviewRepository _reviewRepo = new ReviewRepository();
        IUserRepository _userRepo = new UserRepository();   

        public bool AddCategory(string categoryName)
        {
            if (_categoryRepo.GetByName(categoryName) != null)
            {
                throw new Exception("Category Already Exists!");
            }
            var category = new Category { Name = categoryName };
            _categoryRepo.Add(category);
            return true;
        }

        public bool FindCategory(int categoryId)
        {
            var category = _categoryRepo.GetById(categoryId);
            if (category == null)
            {
                throw new Exception("Category with this id does not exist");
            }
            return true;

        }

        public bool AddBookToCategory(int categoryId, string bookTitle)
        {
            if (_bookRepo.GetByTitleAndCategory(bookTitle, categoryId) != null)
            {
                throw new Exception("Book Already exists in this category");
            }

            var book = new Book { Title = bookTitle, CategoryId = categoryId };
            _bookRepo.Add(book);
            return true;
        }

        public List<Category> ShowAllCategories()
        {
            var categories = _categoryRepo.GetAllCategories().ToList();
            if (categories == null)
                throw new Exception("Category does not exist!");
            return categories;
        }

        public List<Book> ShowAllBooks()
        {
            return _bookRepo.GetAllBooks().ToList();
        }

        public List<Book> ShowAllBooksWithCategoryId(int categoryId)
        {
            var books = _bookRepo.GetAllBooks()
                                 .Where(b => b.CategoryId == categoryId)
                                 .ToList();

            if (books.Count == 0)
                throw new Exception("No books in this category");

            return books;
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepo.GetAllReviews().ToList();
        }

        public Review? SelectReview(int reviewId)
        {
            var reviews = _reviewRepo.GetAllReviews();
            foreach (var review in reviews)
            {
                if (review.Id == reviewId)
                {
                    return review;
                }
            }
            return null;
        }

        public bool ConfirmReview(int reviewId)
        {
            var selectedReview = SelectReview(reviewId);
            if (selectedReview != null)
            {
                selectedReview.IsConfirmed = true;
                _reviewRepo.EditReview(selectedReview);
                return true;
            }
            else
            {
                throw new Exception("review not found");
            }
        }

        public List<User> GetAllUsersPenalties()
        {
            return _userRepo.GetAllUsers();
        }


    }
}


