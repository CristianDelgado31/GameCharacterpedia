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
        var reference = await _dbContext.ReferenceIds
        .Where(r => r.UserId == userId && r.CharacterId == characterId)
        .FirstOrDefaultAsync();

        if (reference == null)
        {
            return 0; // No se encontró ningún registro que coincida con el userId y el characterId dados
        }

        reference.IsVisible = false; // Cambiar el valor de IsVisible a false
        _dbContext.Update(reference); // Marcar la entidad como modificada
        await UnitOfWork.SaveChangesAsync(); // Guardar los cambios en la base de datos

        return 1; // Se cambió el valor de IsVisible a false

    }

    public async Task<ReferenceId> CreateAsync(ReferenceId entity)
    {
        await _dbContext.ReferenceIds.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        return entity;
    }


    public async Task<ReferenceId?> GetByReferenceIdAsync(int userId, int characterId)
    {
        return await _dbContext.ReferenceIds
            .Where(r => r.UserId == userId && r.CharacterId == characterId)
            .FirstOrDefaultAsync();
    }

    public async Task<int> UpdateReferenceAsync(int userId, int characterId)
    {

        var reference = await _dbContext.ReferenceIds
        .Where(r => r.UserId == userId && r.CharacterId == characterId)
        .FirstOrDefaultAsync();

        if (reference == null)
        {
            return 0; // No se encontró ningún registro que coincida con el userId y el characterId dados
        }

        reference.IsVisible = true;
        _dbContext.Update(reference);
        await UnitOfWork.SaveChangesAsync();

        return 1; // Se cambió el valor de IsVisible a true
    }
}