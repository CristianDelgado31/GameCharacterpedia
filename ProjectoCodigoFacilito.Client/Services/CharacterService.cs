using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ProjectoCodigoFacilito.Client.Models.CharacterModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using Blazored.LocalStorage;
using ProjectoCodigoFacilito.Client.Models.UserModel;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorage;

        public CharacterService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }

        public async Task<List<GetCharacterModel>> GetCharacters()
        {
            var characterList = await _httpClient.GetFromJsonAsync<List<GetCharacterModel>>("api/Character");
            await _localStorage.SetItemAsync("CharactersList", characterList);
            return characterList;
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
                var user = await _localStorage.GetItemAsync<SignInUserModel>("UserFavouriteCharacters");
                command.CreatedById = user.Id;

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
