using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Application.Common.Exceptions;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class CharacterController : ApiControllerBase
{
     [HttpGet]
     public async Task<ActionResult<List<CharacterDTO>>> GetAllCharacters()
     {
         var characters = await Mediator.Send(new GetCharacterQuery());
         return Ok(characters);
     }

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDTO>> GetCharacterById(int id)
    {
        var character = await Mediator.Send(new GetCharacterByIdQuery { CharacterId = id });
        return Ok(character);
    }

    [HttpPost]
    public async Task<ActionResult<CharacterDTO>> CreateCharacter(CreateCharacterCommand command)
    {
        try
        {
            var character = await Mediator.Send(command);
            return Ok(character);

        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new
            {
                RequestType = ex.RequestType,
                Errors = ex.Errors
            };

            return BadRequest(errorResponse);
        }

    }

}