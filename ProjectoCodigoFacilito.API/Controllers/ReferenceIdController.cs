using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.ModifyReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceIdById;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class ReferenceIdController : ApiControllerBase
{
    public ReferenceIdController(IConfiguration configuration)
    {
        _config = configuration;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<ReferenceIdDTO>>> GetAll()
    {
        return await Mediator.Send(new GetReferenceIdQuery());
        
    }

    [HttpGet("{userId}/{characterId}")]
    [Authorize]
    public async Task<ActionResult<ReferenceIdDTO>> GetReferenceIdById(int userId, int characterId)
    {
        var result = await Mediator.Send(new GetReferenceIdByIdQuery { UserId = userId, CharacterId = characterId });
        if(result == null)
        {
            result = new ReferenceIdDTO(0, 0, 0, false);
            return Ok(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReferenceIdDTO>> Create(CreateReferenceIdCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpPut]
    [Authorize]
    public async Task<ActionResult<int>> ModifyReferenceId(ModifiyReferenceIdCommand command)
    {
        var result = await Mediator.Send(command);

        if(result == 0)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    
    [HttpDelete("{userId}/{characterId}")]
    [Authorize(Roles = "Administrator, User")]
    public async Task<ActionResult<int>> Delete(int userId, int characterId)
    {
        var result = await Mediator.Send(new DeleteReferenceIdCommand { UserId = userId, CharacterId = characterId });
        if(result == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
}