using MediatR;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

public record GetUserQuery : IRequest<List<UserDTO>>; // MediatR IRequest interface, esto hace que el handler sepa que es un query y no un command