using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId;

public class DeleteReferenceIdCommand : IRequest<int>
{
    public int UserId { get; set; }
    
    public int CharacterId { get; set; }
}