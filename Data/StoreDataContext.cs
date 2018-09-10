using Microsoft.EntityFrameworkCore;
using Expenses.Models;
using Expenses.Data.Maps;

namespace Expenses.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new Config();
            optionsBuilder.UseSqlServer(config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExpenseMap());
        }
    }
}