using FluentValidation;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterValidator : AbstractValidator<DeleteCharacterCommand>
    {
        public DeleteCharacterValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El id del personaje es requerido");
            RuleFor(x => x.ModifiedById).NotEmpty().WithMessage("El usuario que modifica es requerido");
        }
    }
}
