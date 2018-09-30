using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Expenses.ViewModels.RevenueViewModels
{
    public class EditorRevenueViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Name, 3, "Nome", "O nome da receita deve conter mais de 3 caracteres.")
                    .HasMaxLen(Name, 120, "Nome", "O nome da receita não pode conter mais de 120 caracteres.")
                    .IsGreaterThan(Value, 0, "Valor", "O valor da receita deve ser maior que 0.")
            );
        }
    }
}