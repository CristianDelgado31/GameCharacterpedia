namespace ProjectoCodigoFacilito.Domain.Entities;

public abstract class BaseEntity
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }

    public string Role { get; set; }
}