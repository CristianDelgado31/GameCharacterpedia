using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Domain.Repository;

namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, List<CharacterDTO>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    
    public GetCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }
    public async Task<List<CharacterDTO>> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
    {
        var characters = await _characterRepository.GetAllAsync();

        return _mapper.Map<List<CharacterDTO>>(characters);
    }
}