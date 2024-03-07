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
     => await _dbContext.Characters.Where(character => character.IsDeleted == false).ToListAsync();

    public async Task<Character> GetByIdAsync(int id)
    {
        var character = await _dbContext.Characters.FindAsync(id);
        return character;
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

    public async Task<int> UpdateAsync(Character entity)
    {
        return await _dbContext.Characters
            .Where(model => model.Id == entity.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Name, entity.Name)
                .SetProperty(m => m.Game, entity.Game)
                .SetProperty(m => m.Role, entity.Role)
                .SetProperty(m => m.History, entity.History)
                .SetProperty(m => m.ModifiedById, entity.ModifiedById)
                .SetProperty(m => m.ModifiedDate, entity.ModifiedDate)
                .SetProperty(m => m.ImageUrl, entity.ImageUrl)
            );

    }

    public async Task<int> DeleteAsync(Character entity)
    {
        return await _dbContext.Characters
            .Where(model => model.Id == entity.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.IsDeleted, true)
                .SetProperty(m => m.ModifiedById, entity.ModifiedById)
                .SetProperty(m => m.ModifiedDate, entity.ModifiedDate)
            );
    }
}
