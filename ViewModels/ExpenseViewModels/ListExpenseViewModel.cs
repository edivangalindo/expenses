namespace Expenses.ViewModels.ExpenseViewModels
{
    public class ListExpenseViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
    }
}