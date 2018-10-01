using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Expenses.Models;
using Expenses.ViewModels.ExpenseViewModels;
using Expenses.ViewModels;
using Expenses.Repositories;

namespace Expenses.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseRepository _repository;
        public ExpenseController(ExpenseRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/expenses")]
        [HttpGet]
        public IEnumerable<ListExpenseViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/expenses/{id}")]
        [HttpGet]
        public Expense Get(int id)
        {
            return _repository.Get(id);
        }
        
        [Route("v1/expenses/month/{month}")]
        [HttpGet]
        public IEnumerable<ListExpenseViewModel> GetExpensesByMonth(int month)
        {
            return _repository.GetExpensesByMonth(month);
        }

        [Route("v1/expenses/month/{month}/total")]
        [HttpGet]
        public decimal GetTotalValueByMonth(int month)
        {
            return _repository.GetTotalValueByMonth(month);
        }

        [Route("v1/expenses")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorExpenseViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a despesa.",
                    Data = model.Notifications
                };
            }

            var expense = new Expense();
            expense.Id = model.Id;
            expense.Name = model.Name;
            expense.Value = model.Value;
            expense.Date = System.DateTime.Now;
            expense.PaymentMethodId = model.PaymentMethodId;
            expense.UserId = model.UserId;

            _repository.Save(expense);

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
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível atualizar a despesa.",
                    Data = model.Notifications
                };
            }

            var expense = _repository.Get(model.Id);
            expense.Name = model.Name;
            expense.Value = model.Value;
            expense.Date = model.Date;
            expense.PaymentMethodId = model.PaymentMethodId;
            expense.UserId = model.UserId;

            _repository.Update(expense);

            return new ResultViewModel
            {
                Success = true,
                Message = "Despesa atualizada com sucesso.",
                Data = expense
            };
        }

        [Route("v1/expenses")]
        [HttpDelete]
        public Expense Delete([FromBody]Expense expense)
        {
            _repository.Delete(expense);
            return expense;
        }
    }
}