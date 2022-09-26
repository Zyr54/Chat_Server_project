using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;

namespace Chat
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }

        public DatabaseContext()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(folder, "mydb.db");
            System.Console.WriteLine(DbPath);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u._userID)
                .HasName("PrimaryKey_userID");

            modelBuilder.Entity<Message>()
                .HasKey(m => m._messageID)
                .HasName("PrimaryKey_messageID");
            
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages);

            modelBuilder.Entity<User>()
                .Property(u => u._userID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Message>()
                .Property(m => m._messageID)
                .ValueGeneratedOnAdd();


        }
    }
}
