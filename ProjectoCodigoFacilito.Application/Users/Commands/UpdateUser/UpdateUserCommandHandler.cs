using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        var updateUserEntity = _mapper.Map<User>(request);

        return await _userRepository.UpdateUserAsync(request.Id, updateUserEntity);
    }
}