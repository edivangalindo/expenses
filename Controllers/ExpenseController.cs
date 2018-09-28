using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels.ExpenseViewModels;
using Expenses.ViewModels;

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
        public IEnumerable<ListExpenseViewModel> Get()
        {
            return _context.Expenses
            .Select(x => new ListExpenseViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PaymentMethodId = x.PaymentMethodId,
                Date = x.Date,
                Value = x.Value
            })
            .AsNoTracking()
            .ToList();
        }

        [Route("v1/expenses/{id}")]
        [HttpGet]
        public Expense Get(int id)
        {
            return _context.Expenses.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/expenses")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorExpenseViewModel model)
        {
            var expense = new Expense();
            expense.Id = model.Id;
            expense.Name = model.Name;
            expense.Value = model.Value;
            expense.Date = model.Date;
            expense.PaymentMethodId = model.PaymentMethodId;

            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Despesa cadastrada com sucesso.",
                Data = expense
            };
        }

        [Route("v1/expenses")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorExpenseViewModel model)
        {
            var expense = _context.Expenses.Find(model.Id);
            expense.Name = model.Name;
            expense.Value = model.Value;
            expense.Date = model.Date;
            expense.PaymentMethodId = model.PaymentMethodId;
            
            _context.Entry<Expense>(expense).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Despesa alterada com sucesso.",
                Data = expense
            };
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