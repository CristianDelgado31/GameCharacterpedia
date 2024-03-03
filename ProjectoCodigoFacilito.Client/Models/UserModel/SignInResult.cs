namespace ProjectoCodigoFacilito.Client.Models.UserModel
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public int Id { get; set; }
    }
}
