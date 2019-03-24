using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksServices.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<BookChapter> BookChapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookChapter>()
                .ToTable("Chapters")
                .HasKey(c => c.Id);
            modelBuilder.Entity<BookChapter>()
                .Property(c => c.Id)
                .HasColumnType("UniqueIdentifier")
                .HasDefaultValueSql("newid()");
            modelBuilder.Entity<BookChapter>()
                .Property(c => c.Title)
                .HasMaxLength(120);
        }
    }
}
