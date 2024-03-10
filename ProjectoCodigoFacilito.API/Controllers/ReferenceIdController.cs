using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Common.Exceptions;
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
        return await Mediator.Send(new GetAllReferenceIdQuery());
        
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
        try
        {
            return await Mediator.Send(command);
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
        catch (RequestFailedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut]
    [Authorize]
    public async Task<ActionResult<int>> ModifyReferenceId(UpdateReferenceIdCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result == 0)
            {
                return NotFound(result);
            }
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
        catch (RequestFailedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
    [HttpDelete("{userId}/{characterId}")]
    [Authorize]
    public async Task<ActionResult<int>> Delete(int userId, int characterId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteReferenceIdCommand { UserId = userId, CharacterId = characterId });
            if (result == 0)
            {
                return NotFound();
            }

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
        catch (RequestFailedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}