using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterDTO>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IFirebaseService _firebaseService;
    public CreateCharacterCommandHandler(ICharacterRepository characterRepository, IFirebaseService firebaseService)
    {
        _characterRepository = characterRepository;
        _firebaseService = firebaseService;
    }
    public async Task<CharacterDTO> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var newCharacter = new Character
        {
            Name = request.Name,
            Game = request.Game,
            IsVisible = true,
            History = request.History,
            Role = request.Role,
            CreatedById = 1,  //request.CreatedById, -> esta hardcodeado
            CreatedDate = DateTime.Now,
            //ImageUrl = await _firebaseService.UploadStorage(request.nameImageStream, request.ImageStream)
        };

        MemoryStream stream = new MemoryStream(request.ImageStream);

        newCharacter.ImageUrl = await _firebaseService.UploadStorage(request.nameImageStream, stream);

        var createdCharacter = await _characterRepository.CreateAsync(newCharacter);
        
        return new CharacterDTO
        {
            Id = createdCharacter.Id,
            Name = createdCharacter.Name,
            Game = createdCharacter.Game,
            IsVisible = createdCharacter.IsVisible,
            History = createdCharacter.History,
            Role = createdCharacter.Role,
            CreatedById = createdCharacter.CreatedById,
            ModifiedById = createdCharacter.ModifiedById,
            ImageUrl = createdCharacter.ImageUrl
        };
    }
}