using ProjectoCodigoFacilito.Client.Models.UserModel.UserInterfaceModel;

namespace ProjectoCodigoFacilito.Client.Models.UserModel
{
    public class BaseUserModel : IBaseUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
