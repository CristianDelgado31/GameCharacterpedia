using ProjectoCodigoFacilito.Client.Models.UserModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUser(CreateUserModel user);
        Task<string> SignInUser(SignInUserModel user);
    }
}
