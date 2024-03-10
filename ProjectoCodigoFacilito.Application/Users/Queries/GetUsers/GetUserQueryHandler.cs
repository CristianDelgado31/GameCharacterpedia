using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<List<UserDTO>>(users);
    }   
}