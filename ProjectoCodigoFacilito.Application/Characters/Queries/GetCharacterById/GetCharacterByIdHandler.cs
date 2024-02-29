using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Domain.Repository;


namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById
{
    public class GetCharacterByIdHandler : IRequestHandler<GetCharacterByIdQuery, CharacterDTO>
    {
        private readonly ICharacterRepository _characterRepository;
        public GetCharacterByIdHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }
        
        public async Task<CharacterDTO> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetByIdAsync(request.CharacterId);
            
            if(character == null)
                return null;

            return new CharacterDTO
            {
                Id = character.Id,
                Name =character.Name,
                Game = character.Game,
                IsVisible = character.IsVisible,
                History = character.History,
                Role = character.Role,
                CreatedById = character.CreatedById,
                ModifiedById = character.ModifiedById,
                ImageUrl = character.ImageUrl
            };
        }
    }
}
