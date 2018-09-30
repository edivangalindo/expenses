using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels.UserViewModels;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Repositories
{
    public class UserRepository
    {
        private readonly StoreDataContext _context;
        public UserRepository(StoreDataContext context)
        {
            _context = context;
        }

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

        public User Get(int id)
        {
            return _context.User.Find(id);
        }

        public void Save(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
    }
}