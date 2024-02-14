using MediatR;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

public record GetReferenceIdQuery : IRequest<List<ReferenceIdDTO>>;