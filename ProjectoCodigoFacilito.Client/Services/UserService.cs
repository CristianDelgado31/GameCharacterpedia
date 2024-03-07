using ProjectoCodigoFacilito.Client.Models.UserModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectoCodigoFacilito.Client.Utility;


namespace ProjectoCodigoFacilito.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string?> CreateUser(CreateUserModel user) //Sign up
        {
            try
            {
                // Validar que las contraseñas coincidan
                if (user.Password != user.ConfirmPassword)
                {
                    return "Error: Passwords do not match";
                }
                
                byte[] bytesUserName = Encoding.UTF8.GetBytes(user.Name);
                byte[] bytesEmail = Encoding.UTF8.GetBytes(user.Email);
                byte[] bytesPassword = Encoding.UTF8.GetBytes(user.Password);
                var userName = Convert.ToBase64String(bytesUserName);
                var userEmail = Convert.ToBase64String(bytesEmail);
                var userPassword = Convert.ToBase64String(bytesPassword);

                var response = await _httpClient.PostAsJsonAsync("api/User", new CreateUserModel { Name = userName, Email = userEmail, Password = userPassword});
                response.EnsureSuccessStatusCode();

                if(!response.IsSuccessStatusCode)
                {
                    return "Email is already in use";
                }

                return null;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<SignInResult?> SignInUser(SignInUserModel user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User/signin-user", user);
                response.EnsureSuccessStatusCode();

                var loginResult = JsonSerializer.Deserialize<SignInResult>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); 

                if (!response.IsSuccessStatusCode)
                {
                    return loginResult;
                }

                await _localStorage.SetItemAsync("authToken", loginResult!.Token);
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Email!);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", loginResult.Token);

                var getTokenId = await _httpClient.GetFromJsonAsync<SignInUserModel>($"api/User/GetTokenId/{loginResult.Token}");

                var userCharacterList = await GetUserFavouriteCharactersById(getTokenId.Id);


                await _localStorage.SetItemAsync("UserFavouriteCharacters", userCharacterList);

                return loginResult;
            }
            catch (Exception ex)
            {
                return new SignInResult { Success = false, Error = ex.Message };
            }
            

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("UserFavouriteCharacters");
            await _localStorage.RemoveItemAsync("CharactersList");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<SignInUserModel?> GetUserFavouriteCharactersById(int id)
        {
            return await _httpClient.GetFromJsonAsync<SignInUserModel>($"api/User/{id}");
        }


        public async Task<bool> UpdateProfile(SignInUserModel user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User/{user.Id}", user);
            response.EnsureSuccessStatusCode();
            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.Content.ToString());
            }
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            response.EnsureSuccessStatusCode();
            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.Content.ToString());
            }
            await Logout();

            return true;
        }
    }

}
