using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository.SingleInterfaceResponsibility;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IUserRepository : IBaseRepository<User>, IDeleteAsync, IGetByIdAsync<User>
{
    Task<int> UpdateUserAsync(int id, User entity);

    Task<User?> GetUserSignIn(User entity);

}