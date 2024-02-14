using System.ComponentModel.DataAnnotations;

namespace ProjectoCodigoFacilito.Domain.Entities;

public class ReferenceId
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CharacterId { get; set; }
}