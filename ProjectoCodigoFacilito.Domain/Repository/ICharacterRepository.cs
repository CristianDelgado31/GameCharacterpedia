using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository.SingleInterfaceResponsibility;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface ICharacterRepository : IBaseRepository<Character>, IGetByIdAsync<Character>
{
    Task<int> UpdateAsync(Character entity);

    Task<int> DeleteAsync(Character entity);
}