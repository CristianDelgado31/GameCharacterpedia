using Microsoft.AspNetCore.Mvc;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
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

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create(CreateUserCommand command)
    {
        var user = await Mediator.Send(command);
        return Ok(user);    
    }
}