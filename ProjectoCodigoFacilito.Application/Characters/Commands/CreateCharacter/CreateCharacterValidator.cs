using FluentValidation;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Game).NotEmpty().WithMessage("El juego es requerido");
            RuleFor(x => x.IsVisible).NotEmpty().WithMessage("La visibilidad es requerida");
            RuleFor(x => x.History).NotEmpty().WithMessage("La historia es requerida");
            RuleFor(x=>x.Role).NotEmpty().WithMessage("El rol es requerido");
            
        }
    }
}
