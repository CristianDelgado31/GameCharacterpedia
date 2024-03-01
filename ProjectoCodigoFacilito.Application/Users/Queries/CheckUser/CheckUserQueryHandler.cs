using MediatR;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public CheckUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(CheckUserQuery request, CancellationToken cancellationToken)
        {

            var userEntity = new User
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _userRepository.GetUserSignIn(userEntity);

            if (result == null)
                return null;

            return new UserDTO(result.Id, result.Name, result.Email, result.Password, result.listFavoriteCharacters,
                result.IsDeleted, result.CreatedDate, result.ModifiedDate);
        }
    }
}
