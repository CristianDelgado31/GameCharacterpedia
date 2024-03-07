using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface ICharacterRepository : IBaseRepository<Character>
{
    Task<int> UpdateAsync(Character entity);

    Task<int> DeleteAsync(Character entity);
}