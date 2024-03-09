using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using ProjectoCodigoFacilito.Client.Models.CharacterModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using Blazored.LocalStorage;
using ProjectoCodigoFacilito.Client.Models.UserModel;
using ProjectoCodigoFacilito.Client.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ProjectoCodigoFacilito.Client.Models.ApiResponse.Character;

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

        public async Task<ResultCreateCharacter> CreateCharacter(CreateCharacterModel command, IBrowserFile imageFile)
        {
            try
            {
                if(imageFile == null)
                {
                    return new ResultCreateCharacter { Success = false, Error = "Error: Image is required" };
                }

                // Establecer atributos en CreateCharacterCommand
                var image = await ConvertIBrowserFileToByteArray(imageFile);
                command.nameImageStream = image.Name;
                command.ImageStream = image.ImageStream;
                var user = await _localStorage.GetItemAsync<SignInUserModel>("UserFavouriteCharacters");
                command.CreatedById = user.Id;

                // Realizar la llamada HTTP
                var json = System.Text.Json.JsonSerializer.Serialize(command);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Character", content);
                //var responseContent = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<ResultCreateCharacter>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(result!.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultCreateCharacter { Success = false, Error = "Error: " + ex.Message };
            }
        }

        public async Task<ResultUpdateCharacter> UpdateCharacter(UpdateCharacterModel character, IBrowserFile browserFile)
        {
            try
            {
                if(browserFile == null)
                {
                    return new ResultUpdateCharacter { Success = false, Error = "Error: Image is required" };
                }

                var image = await ConvertIBrowserFileToByteArray(browserFile);
                character.ImageStream = image.ImageStream;
                character.nameImageStream = image.Name;
                
                var user = await _localStorage.GetItemAsync<SignInUserModel>("UserFavouriteCharacters");
                character.ModifiedById = user.Id;

                // Realizar la llamada HTTP
                var json = System.Text.Json.JsonSerializer.Serialize(character);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/Character/update", content);
                //var responseContent = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<ResultUpdateCharacter>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(result!.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultUpdateCharacter { Success = false, Error = "Error: " + ex.Message };
            }
        }

        public async Task<ResultDeleteCharacter> DeleteCharacter(DeleteCharacterModel character)
        {
            try
            {
                var user = await _localStorage.GetItemAsync<SignInUserModel>("UserFavouriteCharacters");
                character.ModifiedById = user.Id;
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("api/Character/delete", UriKind.Relative),
                    Content = new StringContent(JsonConvert.SerializeObject(character), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                var result = System.Text.Json.JsonSerializer.Deserialize<ResultDeleteCharacter>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(result!.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultDeleteCharacter { Success = false, Error = "Error: " + ex.Message };
            }
        }

        public async Task<ImageInByteArray> ConvertIBrowserFileToByteArray(IBrowserFile imageFile)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.OpenReadStream().CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            return new ImageInByteArray { ImageStream = fileBytes, Name = imageFile.Name };
        }




    }
}
