using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Character>> GetAllAsync()
     => await _dbContext.Characters.ToListAsync();

    public Task<Character> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Character> CreateAsync(Character entity)
    {
        await _dbContext.Characters.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        return entity;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}