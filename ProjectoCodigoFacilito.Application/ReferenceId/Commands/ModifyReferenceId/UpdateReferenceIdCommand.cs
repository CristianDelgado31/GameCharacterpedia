using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId
{
    public class UpdateReferenceIdCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int CharacterId { get; set; }
    }
}
