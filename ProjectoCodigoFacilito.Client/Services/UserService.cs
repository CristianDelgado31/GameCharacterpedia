using ProjectoCodigoFacilito.Client.Models.UserModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateUser(CreateUserModel user)
        {
            try
            { 
                var response = await _httpClient.PostAsJsonAsync("api/User", user);
                response.EnsureSuccessStatusCode();

                if(!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
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
