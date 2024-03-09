using ProjectoCodigoFacilito.Client.Models.ApiResponse;

namespace ProjectoCodigoFacilito.Client.Models.UserModel
{
    public class SignInResult : BaseApiResponse
    {
        public string Token { get; set; }
    }
}
