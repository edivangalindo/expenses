using Microsoft.EntityFrameworkCore;
using Expenses.Models;

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
    }
}