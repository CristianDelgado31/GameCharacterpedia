using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterDTO>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IFirebaseService _firebaseService;
    private readonly IMapper _mapper;
    public CreateCharacterCommandHandler(ICharacterRepository characterRepository, IFirebaseService firebaseService, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _firebaseService = firebaseService;
        _mapper = mapper;
    }
    public async Task<CharacterDTO> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {

        var newCharacter = _mapper.Map<Character>(request);

        MemoryStream stream = new MemoryStream(request.ImageStream);

        newCharacter.ImageUrl = await _firebaseService.UploadStorage(request.nameImageStream, stream);

        Character createdCharacter = await _characterRepository.CreateAsync(newCharacter);

        if (createdCharacter == null)
            return null;


        return _mapper.Map<CharacterDTO>(createdCharacter);
    }
}