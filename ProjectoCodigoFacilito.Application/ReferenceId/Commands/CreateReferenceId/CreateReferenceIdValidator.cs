using FluentValidation;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId
{
    public class CreateReferenceIdValidator : AbstractValidator<CreateReferenceIdCommand>
    {
        public CreateReferenceIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El usuario es requerido");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El usuario id debe ser mayor de 0");
            RuleFor(x => x.CharacterId).NotEmpty().WithMessage("El personaje es requerido");
            RuleFor(x => x.CharacterId).GreaterThan(0).WithMessage("El personaje id debe ser mayor de 0");
        }
    }
}
