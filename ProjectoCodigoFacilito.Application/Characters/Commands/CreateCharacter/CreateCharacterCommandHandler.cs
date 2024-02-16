using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterDTO>
{
    private readonly ICharacterRepository _characterRepository;
    
    public CreateCharacterCommandHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
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
            CreatedById = request.CreatedById,
            CreatedDate = DateTime.Now
        };
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
            ModifiedById = createdCharacter.ModifiedById
        };
    }
}