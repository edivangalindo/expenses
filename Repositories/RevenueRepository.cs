using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels.RevenueViewModels;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Repositories
{
    public class RevenueRepository
    {
        private readonly StoreDataContext _context;
        public RevenueRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListRevenueViewModel> Get()
        {
            return _context.Revenues
                .Select(x => new ListRevenueViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    Date = x.Date,
                    Value = x.Value
                }).AsNoTracking();
        }

        public Revenue Get(int id)
        {
            return _context.Revenues.Find(id);
        }

        public void Save(Revenue revenue)
        {
            _context.Revenues.Add(revenue);
            _context.SaveChanges();
        }

        public void Update(Revenue revenue)
        {
            _context.Entry<Revenue>(revenue).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Revenue revenue)
        {
            _context.Revenues.Remove(revenue);
            _context.SaveChanges();
        }
    }
}