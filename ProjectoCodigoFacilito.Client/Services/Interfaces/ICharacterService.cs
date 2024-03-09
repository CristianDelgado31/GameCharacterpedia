using Microsoft.AspNetCore.Components.Forms;
using ProjectoCodigoFacilito.Client.Models.ApiResponse.Character;
using ProjectoCodigoFacilito.Client.Models.CharacterModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<List<GetCharacterModel>> GetCharacters();
        Task<ResultCreateCharacter> CreateCharacter(CreateCharacterModel command, IBrowserFile imageFile);
        Task<GetCharacterModel> GetCharacterById(int id);
        Task<ResultUpdateCharacter> UpdateCharacter(UpdateCharacterModel character, IBrowserFile browserFile);
        Task<ResultDeleteCharacter> DeleteCharacter(DeleteCharacterModel character);

    }
}
