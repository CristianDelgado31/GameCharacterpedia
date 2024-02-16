using MediatR;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;

public class CreateReferenceIdCommandHandler : IRequestHandler<CreateReferenceIdCommand, ReferenceIdDTO>
{
    private readonly IReferenceIdRepository _repository;
    
    public CreateReferenceIdCommandHandler(IReferenceIdRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ReferenceIdDTO> Handle(CreateReferenceIdCommand request, CancellationToken cancellationToken)
    {
        var referenceId = new Domain.Entities.ReferenceId
        {
            UserId = request.UserId,
            CharacterId = request.CharacterId,
            IsVisible = true
        };
        await _repository.CreateAsync(referenceId);
        
        return new ReferenceIdDTO(referenceId.Id, referenceId.UserId, referenceId.CharacterId, referenceId.IsVisible);
    }
}