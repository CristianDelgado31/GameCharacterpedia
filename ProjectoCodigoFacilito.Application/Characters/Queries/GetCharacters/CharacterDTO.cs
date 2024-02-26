using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

public class CharacterDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Game { get; set; }
    public bool IsVisible { get; set; }
    public string History { get; set; }
    public string Role { get; set; }
    public int CreatedById { get; set; }
    public int ModifiedById { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public string ImageUrl { get; set; }
}