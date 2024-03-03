using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceIdById
{
    public class GetReferenceIdByIdQuery : IRequest<ReferenceIdDTO>
    {
        public int UserId { get; set; }
        public int CharacterId { get; set; }
    }
}
