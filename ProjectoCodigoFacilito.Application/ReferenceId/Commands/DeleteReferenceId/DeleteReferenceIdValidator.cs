using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId
{
    public class DeleteReferenceIdValidator : AbstractValidator<DeleteReferenceIdCommand>
    {
        public DeleteReferenceIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0");
            RuleFor(x => x.CharacterId).NotEmpty().WithMessage("CharacterId is requited");
            RuleFor(x => x.CharacterId).GreaterThan(0).WithMessage("CharacterId must be greater than 0");
        }
    }
}
