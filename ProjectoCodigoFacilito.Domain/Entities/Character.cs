using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoCodigoFacilito.Domain.Entities;

public class Character : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Game { get; set; }
    public bool IsVisible { get; set; } // creo que no hace falta este atributo
    public string History { get; set; }
    public string Role { get; set; }
    public int CreatedById { get; set; }
    public int ModifiedById { get; set; }
    public string ImageUrl { get; set; }
}