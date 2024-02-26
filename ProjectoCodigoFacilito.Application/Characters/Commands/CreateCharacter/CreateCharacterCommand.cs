using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommand : IRequest<CharacterDTO>
{
    public string Name { get; set; }
    public string Game { get; set; }
    public bool IsVisible { get; set; } = true;
    public string History { get; set; }
    public string Role { get; set; }
    public int CreatedById { get; set; }
    public byte[] ImageStream { get; set; }
    public string nameImageStream { get; set; }
}