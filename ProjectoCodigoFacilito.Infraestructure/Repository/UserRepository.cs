using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
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
    {
        var users = await _dbContext.Users.ToListAsync();
        var listReferencesId = await _dbContext.ReferenceIds.ToListAsync();
        var listCharacters = await _dbContext.Characters.ToListAsync();
        
        foreach (var user in users)
        {
            user.listFavoriteCharacters = listReferencesId
                .Where(referenceId => referenceId.UserId == user.Id)
                .Join(listCharacters, referenceId => referenceId.CharacterId, character => character.Id, (referenceId, character) => character)
                .ToList();
        }
        
        return users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        var listReferencesId = await _dbContext.ReferenceIds.ToListAsync();
        var listCharacters = await _dbContext.Characters.ToListAsync();
        
        user.listFavoriteCharacters = listReferencesId
            .Where(referenceId => referenceId.UserId == user.Id && referenceId.IsVisible)
            .Join(listCharacters, referenceId => referenceId.CharacterId, character => character.Id, (referenceId, character) => character)
            .ToList();
        
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
    }

    public async Task<int> UpdateUserAsync(int id, User entity)
    {
        return await _dbContext.Users
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Name, entity.Name)
                .SetProperty(m => m.Email, entity.Email)
                .SetProperty(m => m.Password, entity.Password)
                .SetProperty(m => m.ModifiedDate, entity.ModifiedDate)
            );
    }
}