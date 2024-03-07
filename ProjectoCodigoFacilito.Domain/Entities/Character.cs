using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoCodigoFacilito.Domain.Entities;

public class Character : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Game { get; set; }
    public string History { get; set; }
    public int CreatedById { get; set; }
    public int ModifiedById { get; set; }
    public string ImageUrl { get; set; }
}