using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.ReferenceId.Commands.CreateReferenceId;
using ProjectoCodigoFacilito.Application.ReferenceId.Queries.GetReferenceId;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class ReferenceIdController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ReferenceIdDTO>>> GetAll()
    {
        return await Mediator.Send(new GetReferenceIdQuery());
        
    }
    
    [HttpPost]
    public async Task<ActionResult<ReferenceIdDTO>> Create(CreateReferenceIdCommand command)
    {
        return await Mediator.Send(command);
    }
}