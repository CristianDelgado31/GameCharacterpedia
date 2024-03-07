using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.UpdateCharacter
{
    public class UpdateCharacterValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Game).NotEmpty().WithMessage("El juego es requerido");
            RuleFor(x => x.History).NotEmpty().WithMessage("La historia es requerida");
            RuleFor(x => x.Role).NotEmpty().WithMessage("El rol es requerido");
            RuleFor(x => x.ModifiedById).NotEmpty().WithMessage("El usuario que modifica es requerido");
            RuleFor(x => x.ImageStream).NotEmpty().WithMessage("La imagen es requerida");
            RuleFor(x => x.nameImageStream).NotEmpty().WithMessage("El nombre de la imagen es requerido");

        }
    }
}
