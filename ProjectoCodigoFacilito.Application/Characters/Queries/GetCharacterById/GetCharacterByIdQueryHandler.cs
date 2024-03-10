using AutoMapper;
using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Domain.Repository;


namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById
{
    public class GetCharacterByIdQueryHandler : IRequestHandler<GetCharacterByIdQuery, CharacterDTO>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        public GetCharacterByIdQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }
        
        public async Task<CharacterDTO> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetByIdAsync(request.CharacterId);
            
            if(character == null)
                return null;

            return _mapper.Map<CharacterDTO>(character);
        }
    }
}
