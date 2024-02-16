using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

public class GetReferenceIdQueryHandler : IRequestHandler<GetReferenceIdQuery, List<ReferenceIdDTO>>
{
    private readonly IReferenceIdRepository _referenceIdRepositoryRepository;
    
    public GetReferenceIdQueryHandler(IReferenceIdRepository referenceIdRepositoryRepository)
    {
        _referenceIdRepositoryRepository = referenceIdRepositoryRepository;
    }
    
    public async Task<List<ReferenceIdDTO>> Handle(GetReferenceIdQuery request, CancellationToken cancellationToken)
    {
        var referenceIds = await _referenceIdRepositoryRepository.GetAllAsync();
        var ListReferenceIdDto = referenceIds
            .Select(r => new ReferenceIdDTO(r.Id, r.UserId, r.CharacterId, r.IsVisible)).ToList();
        
        return ListReferenceIdDto;
    }
}