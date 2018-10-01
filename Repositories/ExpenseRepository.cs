using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels.ExpenseViewModels;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Repositories
{
    public class ExpenseRepository
    {
        private readonly StoreDataContext _context;
        public ExpenseRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListExpenseViewModel> Get()
        {
            return _context.Expenses
                .Select(x => new ListExpenseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Value = x.Value,
                    PaymentMethodId = x.PaymentMethodId,
                    UserId = x.UserId
                })
                .AsNoTracking();
        }

        public Expense Get(int id)
        {
            return _context.Expenses.Find(id);
        }

        public IEnumerable<ListExpenseViewModel> GetExpensesByMonth(int month)
        {
            return _context.Expenses
                .Where(x => x.Date.Month == month)
                .Select(x => new ListExpenseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Value = x.Value,
                    PaymentMethodId = x.PaymentMethodId,
                    UserId = x.UserId
                })
                .AsNoTracking();
        }

        public decimal GetTotalValueByMonth(int month)
        {
            return GetExpensesByMonth(month).Sum(x => x.Value);
        }

        public void Save(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public void Update(Expense expense)
        {
            _context.Entry<Expense>(expense).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Expense expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
    }
}