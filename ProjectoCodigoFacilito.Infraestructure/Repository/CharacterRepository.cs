using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;

namespace ProjectoCodigoFacilito.Infraestructure.Repository;

public class CharacterRepository : ICharacterRepository
{
    private readonly ProjectDbContext _dbContext;
    public CharacterRepository(ProjectDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        UnitOfWork = unitOfWork;
        
    }
    public IUnitOfWork UnitOfWork { get; }

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