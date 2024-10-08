using MediatR;

namespace ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<int>
{
    public int Id { get; set; }
}