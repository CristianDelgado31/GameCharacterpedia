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
    public async Task<ActionResult<ResultCreateCharacter>> CreateCharacter(CreateCharacterCommand command)
    {
        try
        {
            var character = await Mediator.Send(command);

            if (character == null)
                return BadRequest(new ResultCreateCharacter { Error = "This name is already in use", Success = false });

            return Ok(new ResultCreateCharacter { Error = null, Success = true});

        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new ResultCreateCharacter
            {
                Success = false,
                Error = ex.Errors.FirstOrDefault()
            };

            return BadRequest(errorResponse);
        }

    }

    [HttpPut("update")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ResultUpdateCharacter>> UpdateCharacter(UpdateCharacterCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result == 0)
            {
                return BadRequest(new ResultUpdateCharacter { Error = "Error updating character", Success = false });
            }
            else if(result == 2)
            {
                return new ResultUpdateCharacter { Error = "This name is already in use", Success = false };
            }

            return Ok(new ResultUpdateCharacter { Success = true, Error = null });
        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new ResultUpdateCharacter
            {
                Success = false,
                Error = ex.Errors.FirstOrDefault()
            };

            return BadRequest(errorResponse);
        }
        
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ResultDeleteCharacter>> DeleteCharacter(DeleteCharacterCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result == 0)
                return BadRequest(new ResultDeleteCharacter { Error = "Error deleting character" , Success = false});

            return Ok(new ResultDeleteCharacter { Error = null, Success = true});
        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new ResultDeleteCharacter
            {
                Success = false,
                Error = ex.Errors.FirstOrDefault()
            };

            return BadRequest(errorResponse);
        }
        
    }

}