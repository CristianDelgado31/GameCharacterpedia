using ProjectoCodigoFacilito.Client.Models.ApiResponse.User;
using ProjectoCodigoFacilito.Client.Models.UserModel;

namespace ProjectoCodigoFacilito.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUser(CreateUserModel user);
        Task<SignInResult> SignInUser(SignInUserModel user);
        Task Logout();
        Task<UserUpdateResult> UpdateProfile(SignInUserModel user);
        Task<ResultDeleteUser> DeleteUser(int id);
        Task<SignInUserModel?> GetUserFavouriteCharactersById(int id);

    }
}
