using Flunt.Notifications;
using Flunt.Validations;

namespace Expenses.ViewModels.UserViewModels
{
    public class EditorUserViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Name, 3, "Nome", "O nome deve conter mais de 3 caracteres.")
                    .HasMaxLen(Name, 120, "Nome", "O nome não pode conter mais de 120 caracteres.")
                    .HasMinLen(LastName, 3, "Sobrenome", "O sobrenome deve conter mais de 3 caracteres.")
                    .HasMaxLen(LastName, 120, "Sobrenome", "O sobrenome não pode conter mais de 120 carateres.")
                    .IsEmail(Email, "E-mail", "O e-mail informado é inválido")
                    .IsNotNullOrEmpty(Password, "Senha", "A senha não foi informada.")
            );
        }
    }
}