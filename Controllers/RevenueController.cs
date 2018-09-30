using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.Repositories;
using Expenses.ViewModels;
using Expenses.ViewModels.RevenueViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Controllers
{
    public class RevenueController : Controller
    {
        private readonly RevenueRepository _repository;
        public RevenueController(RevenueRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/revenues")]
        [HttpGet]
        public IEnumerable<ListRevenueViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/revenues/{id}")]
        [HttpGet]
        public Revenue Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/revenues")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorRevenueViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a receita.",
                    Data = model.Notifications
                };
            }

            var revenue = new Revenue();
            revenue.Id = model.Id;
            revenue.Name = model.Name;
            revenue.UserId = model.UserId;
            revenue.Value = model.Value;
            revenue.Date = System.DateTime.Now;

            _repository.Save(revenue);

            return new ResultViewModel
            {
                Success = true,
                Message = "Receita cadastrada com sucesso.",
                Data = revenue
            };
        }

        [Route("v1/revenues")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorRevenueViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível atualizar a receita.",
                    Data = model.Invalid
                };
            }
            
            var revenue = _repository.Get(model.Id);
            revenue.Name = model.Name;
            revenue.UserId = model.UserId;
            revenue.Value = model.Value;
            revenue.Date = model.Date;

            _repository.Update(revenue);

            return new ResultViewModel
            {
                Success = true,
                Message = "Receita atualizada com sucesso.",
                Data = revenue
            };
        }

        [Route("v1/revenues")]
        [HttpDelete]
        public Revenue Delete([FromBody]Revenue revenue)
        {
            _repository.Delete(revenue);
            return revenue;
        }
    }
}