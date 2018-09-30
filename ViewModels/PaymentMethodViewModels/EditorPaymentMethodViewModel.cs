using Flunt.Notifications;
using Flunt.Validations;

namespace Expenses.ViewModels.PaymentMethodViewModels
{
    public class EditorPaymentMethodViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Name, 3, "Nome", "O nome do método de pagamento deve conter mais de 3 caracteres.")
                    .HasMaxLen(Name, 120, "Nome", "O nome do método de pagamento não pode conter mais de 120 caracteres.")
            );
        }
    }
}