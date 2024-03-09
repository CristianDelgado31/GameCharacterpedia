using FluentValidation;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

            RuleFor(x => x.Game)
                .NotEmpty().WithMessage("El juego es requerido")
                .MinimumLength(2).WithMessage("El juego debe tener al menos 2 caracteres");

            RuleFor(x => x.History)
                .NotEmpty().WithMessage("La historia es requerida")
                .MinimumLength(5).WithMessage("La historia debe tener al menos 5 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es requerido")
                .MinimumLength(2).WithMessage("El rol debe tener al menos 2 caracteres");

            RuleFor(x => x.CreatedById)
                .NotEmpty().WithMessage("El usuario es requerido")
                .GreaterThan(0).WithMessage("El usuario debe ser mayor a 0");

            RuleFor(x => x.ImageStream)
                .NotEmpty().WithMessage("La imagen es requerida");

            RuleFor(x => x.nameImageStream)
                .NotEmpty().WithMessage("El nombre de la imagen es requerido");

        }
    }
}
