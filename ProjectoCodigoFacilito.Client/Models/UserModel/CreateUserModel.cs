namespace ProjectoCodigoFacilito.Client.Models.UserModel
{
    public class CreateUserModel : BaseUserModel
    {
        public string Name { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
