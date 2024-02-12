using ProjectoCodigoFacilito.Domain.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoCodigoFacilito.Domain.Entities;

public class User : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Character> listFavoriteCharacters { get; set; }
    
}