using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ProjectoCodigoFacilito.Client.Models.CharacterModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetCharacterModel>> GetCharacters()
        {
            return await _httpClient.GetFromJsonAsync<List<GetCharacterModel>>("api/Character");
        }

        public async Task<GetCharacterModel> GetCharacterById(int id)
        {
            return await _httpClient.GetFromJsonAsync<GetCharacterModel>($"api/Character/{id}");
        }

        public async Task<string> CreateCharacter(CreateCharacterModel command, IBrowserFile imageFile)
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
