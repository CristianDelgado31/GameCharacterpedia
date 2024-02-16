using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IReferenceIdRepository : IBaseRepository<ReferenceId>
{
    public Task<int> DeleteReferenceAsync(int userId, int characterId);
    
    //public Task<int> DeleteReferenceAsync(int userId, int characterId);
}