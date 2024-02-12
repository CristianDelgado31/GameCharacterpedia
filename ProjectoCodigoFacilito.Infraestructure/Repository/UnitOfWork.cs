using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;


namespace ProjectoCodigoFacilito.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _dbContext;

        public UnitOfWork(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
