using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;

public class CreateReferenceIdCommandHandler : IRequestHandler<CreateReferenceIdCommand, ReferenceIdDTO>
{
    private readonly IReferenceId _repository;
    
    public CreateReferenceIdCommandHandler(IReferenceId repository)
    {
        _repository = repository;
    }
    
    public async Task<ReferenceIdDTO> Handle(CreateReferenceIdCommand request, CancellationToken cancellationToken)
    {
        var referenceId = new Domain.Entities.ReferenceId
        {
            UserId = request.UserId,
            CharacterId = request.CharacterId
        };
        await _repository.CreateAsync(referenceId);
        
        return new ReferenceIdDTO(referenceId.Id, referenceId.UserId, referenceId.CharacterId);
    }
}