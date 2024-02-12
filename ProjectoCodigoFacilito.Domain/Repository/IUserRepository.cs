using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<int> UpdateCharacterAsync(int id, Character entity);
}