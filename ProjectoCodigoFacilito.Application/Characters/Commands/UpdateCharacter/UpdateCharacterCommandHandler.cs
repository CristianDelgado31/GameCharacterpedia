using MediatR;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.UpdateCharacter
{
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, int>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFirebaseService _firebaseService;
        public UpdateCharacterCommandHandler(ICharacterRepository characterRepository, IFirebaseService firebaseService)
        {
            _characterRepository = characterRepository;
            _firebaseService = firebaseService;
        }
        public async Task<int> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var newCharacter = new Character
            {
                Id = request.Id,
                Name = request.Name,
                Game = request.Game,
                History = request.History,
                Role = request.Role,
                ModifiedById = request.ModifiedById,
                ModifiedDate = DateTime.Now,
            };

            MemoryStream stream = new MemoryStream(request.ImageStream);
            newCharacter.ImageUrl = await _firebaseService.UploadStorage(request.nameImageStream, stream);

            return await _characterRepository.UpdateAsync(newCharacter);
        }
    }
}
