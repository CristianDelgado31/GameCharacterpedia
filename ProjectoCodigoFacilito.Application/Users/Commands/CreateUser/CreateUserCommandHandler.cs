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
        
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                ModifiedDate = DateTime.Now,
            };
            
            var result = await _userRepository.CreateAsync(userEntity);
            
            return new UserDTO(result.Id, result.Name, result.Email, result.Password, result.listFavoriteCharacters,
                result.IsDeleted, result.CreatedDate, result.ModifiedDate);

        }
    }
}
