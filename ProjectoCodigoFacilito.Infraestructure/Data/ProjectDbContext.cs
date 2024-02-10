using Microsoft.EntityFrameworkCore;
using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Infraestructure.Data;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Character> Characters { get; set; }
    
}