using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;


namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId
{
    public class ModifyReferenceIdCommandHandler : IRequestHandler<ModifiyReferenceIdCommand, int>
    {
        private readonly IReferenceIdRepository _referenceIdRepository;
        public ModifyReferenceIdCommandHandler(IReferenceIdRepository referenceIdRepository)
        {
            _referenceIdRepository = referenceIdRepository;
        }
        public async Task<int> Handle(ModifiyReferenceIdCommand request, CancellationToken cancellationToken)
        {
            var referenceId = await _referenceIdRepository.UpdateReferenceAsync(request.UserId, request.CharacterId);

            return referenceId;
        }
    }
}
