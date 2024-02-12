using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        //private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository userRepository)//, IMapper mapper)
        {
            _userRepository = userRepository;
            //_mapper = mapper;
        }
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            
            var result = await _userRepository.CreateAsync(userEntity);
            
            //await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            //return _mapper.Map<UserDTO>(result);
            return new UserDTO(result.Id, result.Name, result.Email, result.Password, new List<Character>(), result.IsDeleted, result.CreatedDate);

        }
    }
}
