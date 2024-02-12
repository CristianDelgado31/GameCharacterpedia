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

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        return user;
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();
        
        return entity;
    }

    public async Task<int> DeleteAsync(int id) // Soft delete (IsDeleted = true) no se borra fisicamente
    {
        return await _dbContext.Users
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.IsDeleted, true));
        
        // return await _dbContext.Users
        //     .Where(model => model.Id == id)
        //     .ExecuteDeleteAsync(); // ExecuteDeleteAsync() usa saveChangesAsync() internamente
    }

    public Task<int> UpdateCharacterAsync(int id, Character entity)
    {
        throw new NotImplementedException();
    }
}