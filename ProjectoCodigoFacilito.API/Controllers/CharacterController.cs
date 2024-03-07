using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Commands.DeleteCharacter;
using ProjectoCodigoFacilito.Application.Characters.Commands.UpdateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacterById;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Application.Common.Exceptions;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class CharacterController : ApiControllerBase
{
    public CharacterController(IConfiguration configuration)
    {
        _config = configuration;
    }

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
    [Authorize(Roles = "Administrator")]
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

    [HttpPut("update")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> UpdateCharacter(UpdateCharacterCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result == 0)
                return BadRequest("Error updating character");

            return Ok(result);
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

    [HttpDelete("delete")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<int>> DeleteCharacter(DeleteCharacterCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result == 0)
                return BadRequest("Error deleting character");

            return Ok(result);
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