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
    
    public DbSet<ReferenceId> ReferenceIds { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasOne<User>() // Especifica la entidad relacionada (User)
            .WithMany(u => u.listFavoriteCharacters)
            // '-> Especifica la propiedad de navegación en User que apunta a una colección de Character
            .HasForeignKey(c => c.CreatedById);
    }

}