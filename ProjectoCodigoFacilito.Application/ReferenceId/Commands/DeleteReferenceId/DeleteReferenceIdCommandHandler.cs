using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId;

public class DeleteReferenceIdCommandHandler : IRequestHandler<DeleteReferenceIdCommand, int>
{
    private readonly IReferenceIdRepository _referenceIdRepository;
    
    public DeleteReferenceIdCommandHandler(IReferenceIdRepository referenceIdRepository)
    {
        _referenceIdRepository = referenceIdRepository;
    }
    
    public async Task<int> Handle(DeleteReferenceIdCommand request, CancellationToken cancellationToken)
    {
        return await _referenceIdRepository.DeleteReferenceAsync(request.UserId, request.CharacterId);
        
    }
}