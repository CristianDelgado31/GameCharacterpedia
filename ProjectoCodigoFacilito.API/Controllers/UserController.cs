using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Common.Exceptions;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;
using ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> Get()
    {
        try
        {
            var users = await Mediator.Send(new GetUserQuery());
            return Ok(users);
        }catch(ValidationExceptionFV ex)
        {
            var errorResponse = new
            {
                RequestType = ex.RequestType,
                Errors = ex.Errors
            };

            return BadRequest(errorResponse);
        }
       
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        try
        {
            var userDto = await Mediator.Send(new GetUserByIdQuery { UserId = id });

            if (userDto == null)
                return NotFound();


            return Ok(userDto);

        }catch(ValidationExceptionFV ex)
        {
            var errorResponse = new
            {
                RequestType = ex.RequestType,
                Errors = ex.Errors
            };

            return BadRequest(errorResponse);
        }

    }

    [HttpPost("check")]
    public async Task<ActionResult<UserDTO>> CheckUser(CheckUserQuery checkUser)
    {   
        var userDto = await Mediator.Send(checkUser);

        if (userDto == null)
            return NotFound();

        return Ok(userDto);

    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create(CreateUserCommand command)
    {
        try
        {
            var user = await Mediator.Send(command);

            return Ok(user);
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
    
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Update(int id, UpdateUserCommand command)
    {
        try
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);

            if (result == 0)
                return NotFound();

            return Ok(result);
        }
        catch(ValidationExceptionFV ex) { 
            
            var errorResponse = new
            {
                RequestType = ex.RequestType,
                Errors = ex.Errors
            };

            return BadRequest(errorResponse);
        
        }
       
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        try
        {

            var result = await Mediator.Send(new DeleteUserCommand { Id = id });

            if (result == 0)
                return NotFound();

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