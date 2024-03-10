using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateCharacterCommandHandler(ICharacterRepository characterRepository, IFirebaseService firebaseService, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var newCharacter = _mapper.Map<Character>(request);

            MemoryStream stream = new MemoryStream(request.ImageStream);
            newCharacter.ImageUrl = await _firebaseService.UploadStorage(request.nameImageStream, stream);

            return await _characterRepository.UpdateAsync(newCharacter);
        }
    }
}
