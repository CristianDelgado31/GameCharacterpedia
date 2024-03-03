using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, List<CharacterDTO>>
{
    private readonly ICharacterRepository _characterRepository;
    
    public GetCharacterQueryHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }
    public async Task<List<CharacterDTO>> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
    {
        var characters = await _characterRepository.GetAllAsync();
        return characters.Select(c => new CharacterDTO
        {
            Id = c.Id,
            Name = c.Name,
            Game = c.Game,
            //IsVisible = c.IsVisible,
            History = c.History,
            Role = c.Role,
            CreatedById = c.CreatedById,
            ModifiedById = c.ModifiedById,
            ImageUrl = c.ImageUrl
        }).ToList();
    }
}