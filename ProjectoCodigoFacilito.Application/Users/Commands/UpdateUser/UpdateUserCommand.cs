using MediatR;
using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime ModifiedDate { get; set; } = DateTime.Now;

}