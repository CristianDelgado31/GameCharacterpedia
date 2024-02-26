
namespace ProjectoCodigoFacilito.Client
{
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;

        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string imageName)
        {

            if(imageStream == null)
            {
                throw new ArgumentNullException(nameof(imageStream));
            }

            return imageName;
        }
    }
}
