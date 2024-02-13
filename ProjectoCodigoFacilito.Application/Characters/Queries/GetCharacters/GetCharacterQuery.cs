using MediatR;

namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

public record GetCharacterQuery : IRequest<List<CharacterDTO>>;