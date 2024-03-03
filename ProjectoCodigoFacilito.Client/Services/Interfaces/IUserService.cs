using ProjectoCodigoFacilito.Client.Models.UserModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUser(CreateUserModel user);
        Task<SignInResult> SignInUser(SignInUserModel user);
        Task Logout();
    }
}
