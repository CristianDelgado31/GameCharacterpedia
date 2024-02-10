using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Domain.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Character> listFavoriteCharacters { get; set; }
    
}