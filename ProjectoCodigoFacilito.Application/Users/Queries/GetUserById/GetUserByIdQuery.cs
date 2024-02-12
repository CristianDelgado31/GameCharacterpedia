using MediatR;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDTO>
{
    public int UserId { get; set; }
}