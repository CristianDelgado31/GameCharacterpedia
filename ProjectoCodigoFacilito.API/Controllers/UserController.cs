using Azure.Core;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectoCodigoFacilito.Application.Common.Exceptions;
using ProjectoCodigoFacilito.Application.GetTokenResult;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;
using ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;
using ProjectoCodigoFacilito.Application.Users.Queries.CheckUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn;
using ProjectoCodigoFacilito.Infraestructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
    //private readonly IConfiguration _config;

    public UserController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "User, Administrator")]
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

    [HttpPost("signin-user")]
    public async Task<ActionResult<CheckUserResult>> CheckUser(CheckUserQuery checkUser)
    {   
        var userDto = await Mediator.Send(checkUser);

        if (userDto == null)
            return NotFound();

        var generateToken = new JwtTokenGenerator(_config);
        var token = generateToken.GenerateJwt(userDto);
        
        return Ok(new CheckUserResult
        {
            Success = true,
            Token = token,
            Error = null
        });

    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create(CreateUserCommand command)
    {
        try
        {
            //Descomentar esto despues de hacer la prueba con el JWT en swagger

            var chainEmailBase64 = Convert.FromBase64String(command.Email);
            var chainUserNameBase64 = Convert.FromBase64String(command.Name);
            var chainPasswordBase64 = Convert.FromBase64String(command.Password);
            command.Email = Encoding.UTF8.GetString(chainEmailBase64);
            command.Password = Encoding.UTF8.GetString(chainPasswordBase64);
            command.Name = Encoding.UTF8.GetString(chainUserNameBase64);

            var user = await Mediator.Send(command);

            if (user == null)
                return BadRequest();

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
    [Authorize]
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
    [Authorize]
    public async Task<ActionResult<int>> Delete(int id)
    {
        try
        {

            var result = await Mediator.Send(new DeleteUserCommand { Id = id });

            if (result == 0)
                return NotFound();

            return Ok(result); //devuelve 1 si elimino el user correctamente
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


    [HttpGet("GetTokenId/{jwtToken}")]
    [Authorize]
    public GetTokenResult GetValuesFromToken(string jwtToken)
    {
        var result = new JwtTokenGenerator(_config).GetValuesFromToken(jwtToken);
        return result;
    }


}
