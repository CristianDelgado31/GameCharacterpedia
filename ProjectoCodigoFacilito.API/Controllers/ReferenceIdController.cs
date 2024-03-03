using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.DeleteReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

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
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReferenceIdDTO>> Create(CreateReferenceIdCommand command)
    {
        return await Mediator.Send(command);
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