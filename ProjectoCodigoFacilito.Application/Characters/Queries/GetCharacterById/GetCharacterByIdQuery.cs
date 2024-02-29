using MediatR;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById
{
    public class GetCharacterByIdQuery : IRequest<CharacterDTO>
    {
        public int CharacterId { get; set; }
    }
}
