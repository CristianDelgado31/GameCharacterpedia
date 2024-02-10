namespace ProjectoCodigoFacilito.Domain.Entities;

public class Character : BaseEntity
{
    public int IdCharacter { get; set; }
    public string Game { get; set; }
    public bool IsVisible { get; set; }
    public string History { get; set; }
    public string Role { get; set; }
    public int CreatedById { get; set; }
    public int ModifiedById { get; set; }
    public DateTime ModifiedDate { get; set; }
}