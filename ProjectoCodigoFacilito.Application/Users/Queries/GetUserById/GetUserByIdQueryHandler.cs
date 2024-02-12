using MediatR;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
            return null;
        
        return new UserDTO(user.Id, user.Name, user.Email, user.Password, new List<Character>(), user.IsDeleted, user.CreatedDate);

    }
}