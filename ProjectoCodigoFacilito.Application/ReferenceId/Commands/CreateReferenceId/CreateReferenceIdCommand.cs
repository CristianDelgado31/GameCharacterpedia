using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;

public class CreateReferenceIdCommand : IRequest<ReferenceIdDTO>
{
    public int UserId { get; set; }
    public int CharacterId { get; set; }
}