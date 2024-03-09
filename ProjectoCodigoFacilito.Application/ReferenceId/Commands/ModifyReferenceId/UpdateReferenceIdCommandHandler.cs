using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;


namespace ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId
{
    public class UpdateReferenceIdCommandHandler : IRequestHandler<UpdateReferenceIdCommand, int>
    {
        private readonly IReferenceIdRepository _referenceIdRepository;
        public UpdateReferenceIdCommandHandler(IReferenceIdRepository referenceIdRepository)
        {
            _referenceIdRepository = referenceIdRepository;
        }
        public async Task<int> Handle(UpdateReferenceIdCommand request, CancellationToken cancellationToken)
        {
            var referenceId = await _referenceIdRepository.UpdateReferenceAsync(request.UserId, request.CharacterId);

            return referenceId;
        }
    }
}
