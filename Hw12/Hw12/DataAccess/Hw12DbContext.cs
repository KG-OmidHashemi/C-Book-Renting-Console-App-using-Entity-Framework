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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasMany(u => u.BorrowedBooks)
                      .WithOne(bb => bb.User)
                      .HasForeignKey(bb => bb.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany<Review>()
                      .WithOne()
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins");

                entity.Property(a => a.UserName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(a => a.Password)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");

                entity.HasMany<BorrowedBook>()
                      .WithOne(bb => bb.Book)
                      .HasForeignKey(bb => bb.BookId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany<Review>()
                      .WithOne()
                      .HasForeignKey(r => r.BookId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(b => b.Title)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasMany(c => c.Books)
                      .WithOne(b => b.Category)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.ToTable("BorrowedBooks");
                entity.Property(bb => bb.BorrowDate)
                      .IsRequired();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");

                entity.Property(r => r.Comment)
                      .HasMaxLength(500);

                entity.Property(r => r.Rating)
                      .IsRequired();

                entity.Property(r => r.IsConfirmed)
                      .HasDefaultValue(false);

                entity.Property(r => r.CreatedAt)
                      .IsRequired();
            });
        }
    }
}
