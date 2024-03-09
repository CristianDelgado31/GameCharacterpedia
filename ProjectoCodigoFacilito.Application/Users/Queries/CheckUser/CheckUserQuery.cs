using MediatR;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn
{
    public class CheckUserQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
