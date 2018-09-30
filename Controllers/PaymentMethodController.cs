using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using Expenses.Models;
using System.Collections;
using System.Linq;
using Expenses.ViewModels;
using System.Collections.Generic;
using Expenses.ViewModels.PaymentMethodViewModels;
using Expenses.Repositories;

namespace Expenses.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly PaymentMethodRepository _repository;

        public PaymentMethodController(PaymentMethodRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/paymentMethods")]
        [HttpGet]
        public IEnumerable<ListPaymentMethodViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/paymentMethods/{id}")]
        [HttpGet]
        public PaymentMethod Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/paymentMethods")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorPaymentMethodViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o método de pagamento.",
                    Data = model.Notifications
                };
            }

            var paymentMethod = new PaymentMethod();
            paymentMethod.Id = model.Id;
            paymentMethod.Name = model.Name;

            _repository.Save(paymentMethod);

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
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível atualizar o método de pagamento",
                    Data = model.Notifications
                };
            }

            var paymentMethod = _repository.Get(model.Id);
            paymentMethod.Name = model.Name;

            _repository.Update(paymentMethod);

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
            _repository.Delete(paymentMethod);
            return paymentMethod;
        }
    }
}