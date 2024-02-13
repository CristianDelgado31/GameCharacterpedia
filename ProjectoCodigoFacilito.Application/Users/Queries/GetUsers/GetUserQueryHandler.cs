using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        
        var userList = users.Select(u => new UserDTO(u.Id, u.Name, u.Email, u.Password,u.listFavoriteCharacters,
            u.IsDeleted, u.CreatedDate, u.ModifiedDate)).ToList();
        return userList;
    }   
}