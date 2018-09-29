using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using Expenses.Models;
using System.Collections;
using System.Linq;
using Expenses.ViewModels;
using System.Collections.Generic;
using Expenses.ViewModels.PaymentMethodViewModels;

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
        public IEnumerable<ListPaymentMethodViewModel> Get()
        {
            return _context.PaymentMethods
                .Select(x => new ListPaymentMethodViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).AsNoTracking();
        }

        [Route("v1/paymentMethods/{id}")]
        [HttpGet]
        public PaymentMethod Get(int id)
        {
            return _context.PaymentMethods.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/paymentMethods")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorPaymentMethodViewModel model)
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

        [Route("v1/paymentMethods")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorPaymentMethodViewModel model)
        {
            var paymentMethod = _context.PaymentMethods.Find(model.Id);
            paymentMethod.Name = model.Name;

            _context.Entry<PaymentMethod>(paymentMethod).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Método de pagamento atualizado com sucesso.",
                Data = paymentMethod
            };
        }

        [Route("v1/paymentMethods")]
        [HttpDelete]
        public PaymentMethod Delete([FromBody]PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            _context.SaveChanges();

            return paymentMethod;
        }
    }
}