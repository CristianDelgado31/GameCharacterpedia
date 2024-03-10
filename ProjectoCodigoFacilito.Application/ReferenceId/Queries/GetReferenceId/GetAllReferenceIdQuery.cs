using MediatR;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

public record GetAllReferenceIdQuery : IRequest<List<ReferenceIdDTO>>;