using Microsoft.AspNetCore.Components.Forms;
using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

namespace ProjectoCodigoFacilito.Client.Services
{
    public interface ICharacterService
    {
        Task<List<CharacterDTO>> GetCharacters();
        Task<string> CreateCharacter(CreateCharacterCommand command, IBrowserFile imageFile);
    }
}
