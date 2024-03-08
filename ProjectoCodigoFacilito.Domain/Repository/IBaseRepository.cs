namespace ProjectoCodigoFacilito.Domain.Repository;

public interface IBaseRepository<T>
{
    public IUnitOfWork UnitOfWork { get; }
    Task<List<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);

}