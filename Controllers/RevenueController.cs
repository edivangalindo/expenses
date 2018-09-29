using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels;
using Expenses.ViewModels.RevenueViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Controllers
{
    public class RevenueController : Controller
    {
        private readonly StoreDataContext _context;
        public RevenueController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/revenues")]
        [HttpGet]
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

        [Route("v1/revenues/{id}")]
        [HttpGet]
        public Revenue Get(int id)
        {
            return _context.Revenues.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/revenues")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorRevenueViewModel model)
        {
            var revenue = new Revenue();
            revenue.Id = model.Id;
            revenue.Name = model.Name;
            revenue.UserId = model.UserId;
            revenue.Value = model.Value;
            revenue.Date = System.DateTime.Now;

            _context.Revenues.Add(revenue);
            _context.SaveChanges();

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
            var revenue = _context.Revenues.Find(model.Id);
            revenue.Name = model.Name;
            revenue.UserId = model.UserId;
            revenue.Value = model.Value;
            revenue.Date = model.Date;

            _context.Entry<Revenue>(revenue).State = EntityState.Modified;
            _context.SaveChanges();

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
            _context.Revenues.Remove(revenue);
            _context.SaveChanges();

            return revenue;
        }
    }
}