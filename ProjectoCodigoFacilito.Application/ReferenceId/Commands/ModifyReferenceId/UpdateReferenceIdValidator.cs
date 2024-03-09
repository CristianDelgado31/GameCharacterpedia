using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId
{
    public class UpdateReferenceIdValidator : AbstractValidator<UpdateReferenceIdCommand>
    {
        public UpdateReferenceIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.CharacterId).NotEmpty();

        }
    }
}
