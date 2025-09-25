using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Services;
using Microsoft.Identity.Client;

AuthenticationService authService = new AuthenticationService();
UserService userService = new UserService();
User currentUser = null;
Admin currentAdmin = null;

while (true)
{
    Console.Clear();
    Console.WriteLine("Main Menu");
    Console.WriteLine("1.Register");
    Console.WriteLine("2.Login");
    Console.WriteLine("3.Exit");
    string menuChoice = Console.ReadLine();

    switch (menuChoice)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Register as: 1.User  2.Admin");
            string roleChoice = Console.ReadLine();
            bool successfulRegistration = false;

            try
            {
                switch (roleChoice)
                {
                    case "1":
                        Console.Write("Username:");
                        string username = Console.ReadLine();
                        Console.Write("Password:");
                        string password = Console.ReadLine();
                        authService.RegisterUser(username, password);
                        successfulRegistration = true;
                        break;

                    case "2":
                        Console.Write("Username:");
                        username = Console.ReadLine();
                        Console.Write("Password:");
                        password = Console.ReadLine();
                        authService.RegisterAdmin(username, password);
                        successfulRegistration = true;
                        break;

                    default:
                        Console.WriteLine("Invalid role choice.");
                        break;
                }
                if (successfulRegistration)
                    Console.WriteLine("Registration was successful");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
            break;

        case "2":
            Console.Clear();
            Console.WriteLine("Login as: 1.User  2.Admin");
            string loginChoice = Console.ReadLine();

            try
            {
                switch (loginChoice)
                {
                    case "1":
                        Console.Write("Username:");
                        string loginUser = Console.ReadLine();
                        Console.Write("Password:");
                        string loginPass = Console.ReadLine();
                        currentUser = authService.LoginUser(loginUser, loginPass);
                        if (currentUser != null)
                        {
                            Console.WriteLine("Login Successful!");
                            Console.ReadKey();
                            UserMenu(currentUser);
                        }
                        break;

                    case "2":
                        Console.Write("Username:");
                        loginUser = Console.ReadLine();
                        Console.Write("Password:");
                        loginPass = Console.ReadLine();
                        currentAdmin = authService.LoginAdmin(loginUser, loginPass);
                        if (currentAdmin != null)
                        {
                            Console.WriteLine("Login Successful!");
                            Console.ReadKey();
                            AdminMenu(currentAdmin);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid login choice.");
                        Console.ReadKey();
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            break;



        case "3":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice.");
            Console.ReadKey();
            break;
    }
}

static void UserMenu(User user)
{
    UserService userService = new UserService();
    bool isLoggedIn = true;
    while (isLoggedIn)
    {
        Console.Clear();
        Console.WriteLine("User Menu");
        Console.WriteLine("1.View Categories");
        Console.WriteLine("2.View Books in Category");
        Console.WriteLine("3.Borrow a Book");
        Console.WriteLine("4.View My Borrowed Books");
        Console.WriteLine("5.Add a Comment");
        Console.WriteLine("6.Edit a Comment");
        Console.WriteLine("7.Delete a Comment");
        Console.WriteLine("8.Logout");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowCategory();
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                try
                {
                    ShowBooksWithSelectedCategory();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;


            case "3":
                Console.Clear();
                ShowAllBooks();
                Console.Write("Enter the Id of the Book you want to borrow:");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    try
                    {

                        userService.BorrowBook(user, bookId);
                        Console.WriteLine("Book borrowed successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Invalid input.");
                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                var borrowedBooks = userService.ShowBorrowedBooks(user);
                if (borrowedBooks.Count == 0)
                    Console.WriteLine("You haven't borrowed any books yet.");
                else
                    foreach (var b in borrowedBooks)
                        Console.WriteLine($"{b.BookId}:{b.Book.Title}");
                Console.ReadKey();
                break;


            case "5":
                Console.Clear();
                var books = userService.GetAllBooks();
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Id}: {book.Title}");
                }

                try
                {
                    Console.WriteLine("Enter the book Id You want to Add a comment:");
                    int selectedBookId = int.Parse(Console.ReadLine());

                    var selectedBook = userService.GetBookById(selectedBookId);
                    if (selectedBook == null)
                    {
                        Console.WriteLine("Book not found.");
                        Console.ReadKey();
                        break;
                    }

                    Console.WriteLine("Rate the Book from number 1-5:");
                    int rating = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Your comment:");
                    string comment = Console.ReadLine();

                    userService.AddComment(user, comment, selectedBookId, rating);
                    Console.WriteLine("Comment added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.ReadKey();
                break;
            case "6":
                Console.Clear();
                try
                {
                    var myReviews = userService.GetUserReviews(user);
                    Console.WriteLine("Your reviews:");
                    foreach (var r in myReviews)
                        Console.WriteLine($"{r.Id}: {r.Comment} ({r.Rating}) - Book ID: {r.BookId}");

                    Console.Write("Select the Review ID you want to edit: ");
                    int reviewId = int.Parse(Console.ReadLine());
                    var review = myReviews.FirstOrDefault(r => r.Id == reviewId);

                    if (review == null)
                    {
                        Console.WriteLine("Invalid Review ID. Review not found.");
                    }
                    else
                    {
                        Console.Write("Enter Your new comment:");
                        string comment = Console.ReadLine();

                        Console.Write("Rate the Book from number 1-5:");
                        int rating = int.Parse(Console.ReadLine());

                        userService.EditReview(user, reviewId, comment, review.BookId, rating);
                        Console.WriteLine("Review updated");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
                break;


            case "7":
                Console.Clear();
                try
                {
                    var myReviews = userService.GetUserReviews(user);
                    Console.WriteLine("Your reviews:");
                    foreach (var r in myReviews)
                        Console.WriteLine($"{r.Id}: {r.Comment} ({r.Rating}) - Book ID: {r.BookId}");

                    Console.Write("Select the Review ID you want to Delete:");
                    int reviewId = int.Parse(Console.ReadLine());
                    var review = myReviews.FirstOrDefault(r => r.Id == reviewId);

                    if (review == null)
                    {
                        Console.WriteLine("Invalid Review ID.");
                    }
                    else
                    {
                        userService.DeleteReview(user, reviewId);
                        Console.WriteLine("Review deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
                break;



            case "8":
                isLoggedIn = false;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Console.ReadKey();
                break;
        }
    }

    void ShowAllBooks()
    {
        var books = userService.GetAllBooks();
        Console.WriteLine("Book List:");
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Id}.{b.Title}");
        }

    }

    void ShowBooksWithSelectedCategory()
    {
        ShowCategory();
        Console.Write("Enter Category ID:");
        if (int.TryParse(Console.ReadLine(), out int catId))
        {
            var reviews = userService.GetAllReviews();
            var boooks = userService.GetBooks(catId);

            foreach (var b in boooks)
            {
                Console.WriteLine($"{b.Id}.{b.Title}");
                foreach (var r in reviews)
                {
                    if (r.BookId == b.Id && r.IsConfirmed)
                    {
                        Console.WriteLine($"{r.Username}:\n{r.Rating}/5 star \n {r.Comment}");
                    }
                }
            }

        }
        else
            Console.WriteLine("Invalid input.");
        Console.ReadKey();
    }

    void ShowCategory()
    {
        Console.Clear();
        var categories = userService.GetCategories();
        if (categories.Count == 0)
            Console.WriteLine("No categories available.");
        else
            foreach (var c in categories)
                Console.WriteLine($"{c.Id}:{c.Name}");
    }
}

static void AdminMenu(Admin admin)
{
    AdminService adminService = new AdminService();

    while (true)
    {
        Console.Clear();
        Console.WriteLine("Admin Menu");
        Console.WriteLine("1.View Categories");
        Console.WriteLine("2.Add Category");
        Console.WriteLine("3.View Books in Category");
        Console.WriteLine("4.Add Book to Category");
        Console.WriteLine("5.Confirm a Review");
        Console.WriteLine("6.Logout");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                var categories = adminService.ShowAllCategories();
                if (categories.Count == 0)
                    Console.WriteLine("No categories available.");
                else
                    foreach (var c in categories)
                        Console.WriteLine($"{c.Id}:{c.Name}");
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                Console.Write("Enter new category name:");
                string categoryName = Console.ReadLine();
                try
                {
                    adminService.AddCategory(categoryName);
                    Console.WriteLine("Category added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();
                try
                {
                    var categoreis = adminService.ShowAllCategories();
                    foreach (var c in categoreis)
                    {
                        Console.WriteLine($"{c.Id}.{c.Name}");
                    }
                    Console.WriteLine("Enter the category Id to show it's books:");
                    int SelectedcategoryId = int.Parse(Console.ReadLine());
                    var books = adminService.ShowAllBooksWithCategoryId(SelectedcategoryId);
                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.Id}.{book.Title}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                try
                {
                    categories = adminService.ShowAllCategories();
                    if (categories.Count == 0)
                    {
                        Console.WriteLine("No categories available.");
                    }
                    else
                    {
                        foreach (var c in categories)
                            Console.WriteLine($"{c.Id}:{c.Name}");

                        Console.Write("Enter Category ID: ");
                        int categoryId = int.Parse(Console.ReadLine());

                        adminService.FindCategory(categoryId);

                        Console.Write("Enter Book Title: ");
                        string bookTitle = Console.ReadLine();

                        try
                        {
                            adminService.AddBookToCategory(categoryId, bookTitle);
                            Console.WriteLine("Book added successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }

                Console.ReadKey();
                break;



            case "5":
                Console.Clear();
                try
                {
                    var reviews = adminService.GetAllReviews();
                    foreach (var review in reviews)
                    {
                        Console.WriteLine($"{review.Id}: {review.Username}\n{review.Comment}\n{review.IsConfirmed}");
                    }
                    Console.WriteLine("Enter the review Id to confirm");
                    int selectedReviewId = int.Parse(Console.ReadLine());
                    adminService.ConfirmReview(selectedReviewId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "6":
                return;

            default:
                Console.WriteLine("Invalid choice.");
                Console.ReadKey();
                break;
        }
    }

}
