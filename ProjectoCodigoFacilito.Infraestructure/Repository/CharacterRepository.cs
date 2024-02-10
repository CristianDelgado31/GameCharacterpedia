using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Infraestructure.Repository;

public class CharacterRepository : ICharacterRepository<Character>
{
    public Task<List<Character>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Character> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Character> CreateAsync(Character entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}