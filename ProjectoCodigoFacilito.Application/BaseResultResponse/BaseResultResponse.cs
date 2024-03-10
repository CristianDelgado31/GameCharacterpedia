using ProjectoCodigoFacilito.Domain.Repository.SingleInterfaceResponsibility;

namespace ProjectoCodigoFacilito.Application.BaseResultResponse
{
    public class BaseResultResponse : IBaseResultReponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
