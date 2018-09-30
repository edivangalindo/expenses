using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.Repositories;
using Expenses.ViewModels;
using Expenses.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository;
        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/users")]
        [HttpGet]
        public IEnumerable<ListUserViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/users/{id}")]
        [HttpGet]
        public User Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/users")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorUserViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o usuário.",
                    Data = model.Notifications
                };
            }

            var user = new User();
            user.Id = model.Id;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;

            _repository.Save(user);

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
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Não foi possível atualizar o usuário.",
                    Data = model.Notifications
                };
            }

            var user = _repository.Get(model.Id);
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;

            _repository.Update(user);

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
            _repository.Delete(user);
            return user;
        }
    }
}