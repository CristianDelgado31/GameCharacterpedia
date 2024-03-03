using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;


namespace ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceIdById
{
    public class GetReferenceIdByIdQueryHandler : IRequestHandler<GetReferenceIdByIdQuery, ReferenceIdDTO>
    {
        private readonly IReferenceIdRepository _referenceIdRepository;
        public GetReferenceIdByIdQueryHandler(IReferenceIdRepository referenceIdRepository)
        {
            _referenceIdRepository = referenceIdRepository;
        }
        public async Task<ReferenceIdDTO> Handle(GetReferenceIdByIdQuery request, CancellationToken cancellationToken)
        {
            var reference = await _referenceIdRepository.GetByReferenceIdAsync(request.UserId, request.CharacterId);

            if (reference == null)
                return null;

            return new ReferenceIdDTO(reference.Id, reference.UserId, reference.CharacterId, reference.IsVisible);
        }
    }
}
