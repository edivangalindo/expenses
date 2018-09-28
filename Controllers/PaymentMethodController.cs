using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using Expenses.Models;
using System.Collections;
using System.Linq;
using Expenses.ViewModels;
using System.Collections.Generic;

namespace Expenses.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly StoreDataContext _context;

        public PaymentMethodController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/paymentMethods")]
        [HttpGet]
        public IEnumerable<PaymentMethod> Get()
        {
            return _context.PaymentMethods.AsNoTracking().ToList();
        }

        [Route("v1/paymentMethods")]
        [HttpPost]
        public ResultViewModel Post([FromBody]PaymentMethod model)
        {
            var paymentMethod = new PaymentMethod();
            paymentMethod.Id = model.Id;
            paymentMethod.Name = model.Name;

            _context.PaymentMethods.Add(paymentMethod);
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Método de pagamento adicionado com sucesso.",
                Data = paymentMethod
            };
        }
    }
}