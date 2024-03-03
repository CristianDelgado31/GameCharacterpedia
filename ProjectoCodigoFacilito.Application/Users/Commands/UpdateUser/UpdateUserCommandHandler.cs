using MediatR;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    
    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateUserEntity = new User
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            Password = request.Password,
            ModifiedDate = DateTime.Now
        };
        return await _userRepository.UpdateUserAsync(request.Id, updateUserEntity);
    }
}