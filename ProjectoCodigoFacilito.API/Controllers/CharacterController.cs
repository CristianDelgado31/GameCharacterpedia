using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class CharacterController : ApiControllerBase
{
     [HttpGet]
     public async Task<ActionResult<List<CharacterDTO>>> Get()
     {
         var characters = await Mediator.Send(new GetCharacterQuery());
         return Ok(characters);
     }
     
     [HttpPost]
     public async Task<ActionResult<CharacterDTO>> CreateCharacter(CreateCharacterCommand command)
     { 
         var character = await Mediator.Send(command);
         return Ok(character);
     }
}