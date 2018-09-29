using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels;
using Expenses.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Controllers
{
    public class UserController : Controller
    {
        private readonly StoreDataContext _context;
        public UserController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/users")]
        [HttpGet]
        public IEnumerable<ListUserViewModel> Get()
        {
            return _context.User.Select(x => new ListUserViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password
            })
            .AsNoTracking();
        }

        [Route("v1/users/{id}")]
        [HttpGet]
        public User Get(int id)
        {
            return _context.User.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/users")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorUserViewModel model)
        {
            var user = new User();
            user.Id = model.Id;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;

            _context.User.Add(user);
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Usuário adicionado com sucesso.",
                Data = user
            };
        }

        [Route("v1/users")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorUserViewModel model)
        {
            var user = _context.User.Find(model.Id);
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;

            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Usuário atualizado com sucesso.",
                Data = user
            };
        }

        [Route("v1/users")]
        [HttpDelete]
        public User Delete([FromBody]User user)
        {
            _context.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}