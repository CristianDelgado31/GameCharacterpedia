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
        //Es mas eficiente hacer una sola consulta a la base de datos
        // var users = await _dbContext.Users
        //     .Include(u => u.listFavoriteCharacters) // Carga los personajes favoritos de cada usuario
        //     .ToListAsync();
        //
        // return users;
        
        var users = await _dbContext.Users.ToListAsync();
        
        foreach (var user in users)
        {
            user.listFavoriteCharacters = await _dbContext.Characters
                .Where(character => character.CreatedById == user.Id)
                .ToListAsync();
        }
        
        return users;
    }

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