using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;

namespace ProjectoCodigoFacilito.Infraestructure.Repository;

public class ReferenceIdRepository : IReferenceId
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

    public Task<ReferenceId?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ReferenceId> CreateAsync(ReferenceId entity)
    {
        await _dbContext.ReferenceIds.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        return entity;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}