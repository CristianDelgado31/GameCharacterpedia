using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Users.Commands.GetUser
{
    public class GetUserValidator : AbstractValidator<GetUserCommand>
    {
        public GetUserValidator()
        {
           
        }
    }
}
