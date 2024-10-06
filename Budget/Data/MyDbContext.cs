using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Budget.Models;
using Microsoft.EntityFrameworkCore;
using Transaction = Budget.Models.Transaction;

namespace Budget.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Category> Categories { get; set;}
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}