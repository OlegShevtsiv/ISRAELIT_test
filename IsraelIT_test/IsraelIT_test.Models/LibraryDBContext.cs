using IsraelIT_test.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsraelIT_test.Models
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }


        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../Library.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(t => new { t.BookId, t.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(sc => sc.Book)
                .WithMany(s => s.BookAuthors)
                .HasForeignKey(sc => sc.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(sc => sc.Author)
                .WithMany(c => c.BookAuthors)
                .HasForeignKey(sc => sc.AuthorId);
        }

    }
}
