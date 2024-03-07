using Microsoft.AspNetCore.Components.Forms;
using ProjectoCodigoFacilito.Client.Models.CharacterModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<List<GetCharacterModel>> GetCharacters();
        Task<string> CreateCharacter(CreateCharacterModel command, IBrowserFile imageFile);
        Task<GetCharacterModel> GetCharacterById(int id);
        Task<string> UpdateCharacter(UpdateCharacterModel character, IBrowserFile browserFile);
        Task<string> DeleteCharacter(DeleteCharacterModel character);

    }
}
