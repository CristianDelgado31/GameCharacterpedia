using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser.Validation;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using System.Text;

namespace ProjectoCodigoFacilito.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            User test = _mapper.Map<User>(request);

            var result = await _userRepository.CreateAsync(test);

            return _mapper.Map<UserDTO>(result);
        }
    }
}
