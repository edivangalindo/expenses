using System;

namespace Expenses.ViewModels.RevenueViewModels
{
    public class EditorRevenueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}