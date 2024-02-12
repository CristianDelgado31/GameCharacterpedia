using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;

namespace ProjectoCodigoFacilito.Infraestructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ProjectDbContext _dbContext;

    public UserRepository(ProjectDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        UnitOfWork = unitOfWork;

    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task<List<User>> GetAllAsync()
     => await _dbContext.Users.ToListAsync();

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        
        return entity;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateCharacterAsync(int id, Character entity)
    {
        throw new NotImplementedException();
    }
}