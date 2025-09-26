using Hw12.Enteties;
using Hw12.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.DataAccess
{
    public class Hw12DbContext : DbContext
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> wishlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Book ↔ Category (many-to-one)
                modelBuilder.Entity<Book>()
                    .HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId);

                // BorrowedBook ↔ User (many-to-one)
                modelBuilder.Entity<BorrowedBook>()
                    .HasOne(bb => bb.User)
                    .WithMany(u => u.BorrowedBooks)
                    .HasForeignKey(bb => bb.UserId);

                // BorrowedBook ↔ Book (many-to-one)
                modelBuilder.Entity<BorrowedBook>()
                    .HasOne(bb => bb.Book)
                    .WithMany()
                    .HasForeignKey(bb => bb.BookId);

                // Review (UserId + BookId)
                modelBuilder.Entity<Review>()
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(r => r.BookId);

                modelBuilder.Entity<Review>()
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(r => r.UserId);

                // Wishlist (UserId + BookId)
                modelBuilder.Entity<Wishlist>()
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(w => w.UserId);

                modelBuilder.Entity<Wishlist>()
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(w => w.BookId);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
