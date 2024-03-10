using MediatR;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

public record GetUserQuery : IRequest<List<UserDTO>>;