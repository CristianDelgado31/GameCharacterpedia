using ProjectoCodigoFacilito.Client.Models.ReferenceModel;
using ProjectoCodigoFacilito.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace ProjectoCodigoFacilito.Client.Services
{
    public class ReferenceIdService : IReferenceIdService
    {
        private readonly HttpClient _httpClient;
        public ReferenceIdService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReferenceIdModel?> CreateReferenceId(ReferenceIdModel reference)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ReferenceId", reference);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
                }

                return await response.Content.ReadFromJsonAsync<ReferenceIdModel>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        public async Task<ReferenceIdModel> DeleteReferenceId(ReferenceIdModel reference)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ReferenceId/{reference.UserId}/{reference.CharacterId}");
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
                }

                return reference;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
            
        }

        public async void ModifyReferenceId(ReferenceIdModel reference)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/ReferenceId/", reference);
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.Content.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        public async Task<ReferenceIdModel?> GetReferenceId(ReferenceIdModel reference)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ReferenceIdModel>($"api/ReferenceId/{reference.UserId}/{reference.CharacterId}");

                if(response?.UserId == 0)
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }
    }
}
