using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using System.Net.Http.Json;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharacterDTO>> GetCharacters()
        {
            return await _httpClient.GetFromJsonAsync<List<CharacterDTO>>("api/Character");
        }

        public async Task<string> CreateCharacter(CreateCharacterCommand command, IBrowserFile imageFile)
        {
            try
            {
                // Convertir IBrowserFile a byte[]
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.OpenReadStream().CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                // Establecer atributos en CreateCharacterCommand
                command.nameImageStream = imageFile.Name;
                command.ImageStream = fileBytes;

                // Realizar la llamada HTTP
                var json = JsonSerializer.Serialize(command);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Character", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(responseContent);
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }






    }
}
