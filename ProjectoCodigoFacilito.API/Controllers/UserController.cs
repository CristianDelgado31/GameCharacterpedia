using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> Get()
    {
        var users = await Mediator.Send(new GetUserQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var userDto = await Mediator.Send(new GetUserByIdQuery {UserId = id});
        
        if(userDto == null)
            return NotFound();
        
        return Ok(userDto);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create(CreateUserCommand command)
    {
        var user = await Mediator.Send(command);
        return Ok(user);    
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteUserCommand {Id = id});
        
        if(result == 0)
            return NotFound();
        
        return Ok(result);
    }
}