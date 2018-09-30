using System.Collections.Generic;
using System.Linq;
using Expenses.Data;
using Expenses.Models;
using Expenses.ViewModels.PaymentMethodViewModels;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Repositories
{
    public class PaymentMethodRepository
    {
        private readonly StoreDataContext _context;
        public PaymentMethodRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListPaymentMethodViewModel> Get()
        {
            return _context.PaymentMethods
                .Select(x => new ListPaymentMethodViewModel { Id = x.Id, Name = x.Name })
                .AsNoTracking();
        }

        public PaymentMethod Get(int id)
        {
            return _context.PaymentMethods.Find(id);
        }

        public void Save(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            _context.SaveChanges();
        }

        public void Update(PaymentMethod paymentMethod)
        {
            _context.Entry<PaymentMethod>(paymentMethod).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            _context.SaveChanges();
        }
    }
}