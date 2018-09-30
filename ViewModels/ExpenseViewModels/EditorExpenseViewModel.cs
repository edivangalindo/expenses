using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Expenses.ViewModels.ExpenseViewModels
{
    public class EditorExpenseViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int PaymentMethodId { get; set; }
        public int UserId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMaxLen(Name, 120, "Name", "O nome da despesa deve conter no máximo 120 caracteres.")
                    .HasMinLen(Name, 3, "Name", "O nome da despesa deve conter mais que 3 caracteres.")
                    .IsGreaterThan(Value, 0, "Valor", "O Valor da despesa deve ser maior que 0.")
            );
        }
    }
}