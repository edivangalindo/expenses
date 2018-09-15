using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using Expenses.Models;

namespace Expenses.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly StoreDataContext _context;

        public ExpenseController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/expenses")]
        [HttpGet]
        public IEnumerable<Expense> Get()
        {
            return _context.Expenses.AsNoTracking().ToList();
        }

        [Route("v1/expenses/{id}")]
        [HttpGet]
        public Expense Get(int id)
        {
            return _context.Expenses.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/expenses")]
        [HttpPost]
        public Expense Post([FromBody]Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return expense;
        }

        [Route("v1/expenses")]
        [HttpPut]
        public Expense Put([FromBody]Expense expense)
        {
            _context.Entry<Expense>(expense).State = EntityState.Modified;
            _context.SaveChanges();

            return expense;
        }

        [Route("v1/expenses")]
        [HttpDelete]
        public Expense Delete([FromBody]Expense expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            return expense;
        }
    }
}