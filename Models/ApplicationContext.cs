﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Library_Management_System.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.UserId);

                entity.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                entity.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property<int>("AuthorId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                entity.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.HasKey(e => e.AuthorId);

                entity.ToTable("Authors");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property<int>("BookId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                entity.Property<int>("AuthorId")
                    .HasColumnType("int");

                entity.Property<bool>("Available")
                    .HasColumnType("tinyint(1)");

                entity.Property<int>("GenreId")
                    .HasColumnType("int");

                entity.Property<int>("ISBNumber")
                    .HasColumnType("int");

                entity.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property<int>("Pages")
                    .HasColumnType("int");

                entity.Property<string>("Summary")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.HasKey(e => e.BookId);

                entity.ToTable("Books");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property<int>("GenreId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                entity.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.HasKey(e => e.GenreId);

                entity.ToTable("Genres");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property<int>("TransactionId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                entity.Property<int>("BookId")
                    .HasColumnType("int");

                entity.Property<DateTime>("BorrowDate")
                    .HasColumnType("datetime(6)");

                entity.Property<DateTime>("ReturnDate")
                    .HasColumnType("datetime(6)");

                entity.Property<int>("UserId")
                    .HasColumnType("int");

                entity.HasKey(e => e.TransactionId);

                entity.ToTable("Transactions");
            });
        }
    }
}
