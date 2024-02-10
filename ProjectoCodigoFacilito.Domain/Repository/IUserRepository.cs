using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IUserRepository<T> : IBaseRepository<User>
{
    Task<int> UpdateCharacterAsync(int id, T entity);
}