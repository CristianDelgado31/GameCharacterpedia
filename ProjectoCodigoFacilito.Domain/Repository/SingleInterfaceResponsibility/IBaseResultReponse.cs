using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Domain.Repository.SingleInterfaceResponsibility
{
    public interface IBaseResultReponse
    {
        bool Success { get; set; }
        string? Error { get; set; }
    }
}
