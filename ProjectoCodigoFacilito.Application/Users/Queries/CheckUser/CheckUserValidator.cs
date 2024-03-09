

using FluentValidation;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn;

namespace ProjectoCodigoFacilito.Application.Users.Queries.CheckUser
{
    public class CheckUserValidator : AbstractValidator<CheckUserQuery>
    {
        public CheckUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("La dirección de correo electrónico es obligatoria.")
                .EmailAddress().WithMessage("Se requiere una dirección de correo electrónico válida.")
                .Must(email => email.Contains("@") && email.EndsWith(".com"))
                .WithMessage("La dirección de correo electrónico no es válida.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
