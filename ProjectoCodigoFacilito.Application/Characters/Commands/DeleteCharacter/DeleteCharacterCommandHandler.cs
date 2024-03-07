
using MediatR;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, int>
    {
        private readonly ICharacterRepository _characterRepository;
        public DeleteCharacterCommandHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }
        public async Task<int> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = new Character
            {
                Id = request.Id,
                ModifiedById = request.ModifiedById,
                ModifiedDate = DateTime.Now,
            };
            return await _characterRepository.DeleteAsync(character);
        }
    }
}
