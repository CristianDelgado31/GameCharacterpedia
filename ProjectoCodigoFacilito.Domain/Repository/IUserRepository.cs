using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<int> UpdateUserAsync(int id, User entity);

    Task<User> GetUserSignIn(User entity);
}