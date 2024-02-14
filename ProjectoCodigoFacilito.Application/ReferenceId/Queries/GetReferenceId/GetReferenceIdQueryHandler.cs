using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

public class GetReferenceIdQueryHandler : IRequestHandler<GetReferenceIdQuery, List<ReferenceIdDTO>>
{
    private readonly IReferenceId _referenceIdRepository;
    
    public GetReferenceIdQueryHandler(IReferenceId referenceIdRepository)
    {
        _referenceIdRepository = referenceIdRepository;
    }
    
    public async Task<List<ReferenceIdDTO>> Handle(GetReferenceIdQuery request, CancellationToken cancellationToken)
    {
        var referenceIds = await _referenceIdRepository.GetAllAsync();
        var ListReferenceIdDto = referenceIds
            .Select(r => new ReferenceIdDTO(r.Id, r.UserId, r.CharacterId)).ToList();
        
        return ListReferenceIdDto;
    }
}