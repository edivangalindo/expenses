using System;

namespace Expenses.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        // public Expense(string description, decimal price, PaymentMethod paymentMethod)
        // {
        //     Id = Guid.NewGuid();
        //     Description = description;
        //     Price = price;
        //     Date = DateTime.Now;
        //     PaymentMethod = paymentMethod;

        // }
    }
}