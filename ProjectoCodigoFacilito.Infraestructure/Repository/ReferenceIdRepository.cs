using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;

namespace ProjectoCodigoFacilito.Infraestructure.Repository;

public class ReferenceIdRepository : IReferenceIdRepository
{
    private readonly ProjectDbContext _dbContext;
    
    public ReferenceIdRepository(ProjectDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        UnitOfWork = unitOfWork;
    }
    
    public IUnitOfWork UnitOfWork { get; }
    
    public async Task<List<ReferenceId>> GetAllAsync()
     => await _dbContext.ReferenceIds.ToListAsync();
    
    
    public async Task<int> DeleteReferenceAsync(int userId, int characterId)
    {
        return await _dbContext.ReferenceIds
            .Where(r => r.UserId == userId && r.CharacterId == characterId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(r => r.IsVisible, false));
    }
    
    public async Task<ReferenceId> CreateAsync(ReferenceId entity)
    {
        await _dbContext.ReferenceIds.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        return entity;
    }

    //En uso este get by id
    public async Task<ReferenceId?> GetByReferenceIdAsync(int userId, int characterId)
    {
        return await _dbContext.ReferenceIds
            .Where(r => r.UserId == userId && r.CharacterId == characterId)
            .FirstOrDefaultAsync();
    }

    public async Task<int> UpdateReferenceAsync(int userId, int characterId)
    {
        return await _dbContext.ReferenceIds
            .Where(r => r.UserId == userId && r.CharacterId == characterId)
            .ExecuteUpdateAsync(setters => setters
                           .SetProperty(r => r.IsVisible, true));
    }
}