using System;

namespace Expenses.ViewModels.ExpenseViewModels
{
    public class EditorExpenseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int PaymentMethodId { get; set; }
        public int UserId { get; set; }
    }
}