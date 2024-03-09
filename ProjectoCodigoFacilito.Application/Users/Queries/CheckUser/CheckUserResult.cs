namespace ProjectoCodigoFacilito.Application.Users.Queries.CheckUser
{
    public class CheckUserResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string? Error { get; set; }
    }
}
