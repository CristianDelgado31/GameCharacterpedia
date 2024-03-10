using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using System.Text;

namespace ProjectoCodigoFacilito.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
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
        }
        catch(ValidationExceptionFV ex)
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
    [Authorize]
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
        try
        {
            var userDto = await Mediator.Send(checkUser);

            if (userDto == null)
                return NotFound(new CheckUserResult { Success = false, Token = null, Error = "User not found" });

            var generateToken = new JwtTokenGenerator(_config);
            var token = generateToken.GenerateJwt(userDto);

            return Ok(new CheckUserResult
            {
                Success = true,
                Token = token,
                Error = null
            });
        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new CheckUserResult
            {
                Success = false,
                Token = null,
                Error = ex.Errors.FirstOrDefault()
                
            };

            return BadRequest(errorResponse);
        }

    }

    [HttpPost]
    public async Task<ActionResult<UserCreationResult>> Create(CreateUserCommand command)
    {
        try
        {
            //Vuelve los datos a su estado original antes de la validacion de FV
           var chainEmailBase64 = Convert.FromBase64String(command.Email);
            var chainUserNameBase64 = Convert.FromBase64String(command.Name);
            var chainPasswordBase64 = Convert.FromBase64String(command.Password);
            command.Email = Encoding.UTF8.GetString(chainEmailBase64);
            command.Password = Encoding.UTF8.GetString(chainPasswordBase64);
            command.Name = Encoding.UTF8.GetString(chainUserNameBase64);

            var user = await Mediator.Send(command);

            if (user == null)
                return BadRequest(new UserCreationResult { Error = "That email is already in use.", Success = false});

            return Ok(new UserCreationResult { Error = null, Success = true});
        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new UserCreationResult
            {
                Error = ex.Errors.FirstOrDefault(),
                Success = false
            };

            return BadRequest(errorResponse);
        }
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<UpdateUserResult>> Update(int id, UpdateUserCommand command)
    {
        try
        {
            if (id != command.Id)
                return BadRequest(new UpdateUserResult { Error = "Id and command.Id are not the same", Success = false});

            var result = await Mediator.Send(command);

            if (result == 0)
                return NotFound(new UpdateUserResult { Error = "The chosen email is already in use or there are problems with the server" });

            return Ok(new UpdateUserResult { Error = null, Success = true});
        }
        catch(ValidationExceptionFV ex) 
        {    
            var errorResponse = new UpdateUserResult
            {
                Error = ex.Errors.FirstOrDefault(),
                Success = false
            };

            return BadRequest(errorResponse);
        
        }
       
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ResultDeleteUser>> Delete(int id)
    {
        try
        {

            var result = await Mediator.Send(new DeleteUserCommand { Id = id });

            if (result == 0)
                return NotFound(new ResultDeleteUser { Error = "UserId not found", Success = false});

            //devuelve 1 si elimino el user correctamente
            return Ok(new ResultDeleteUser { Error = null, Success = true});
        }
        catch (ValidationExceptionFV ex)
        {
            var errorResponse = new ResultDeleteUser
            {
                Error = ex.Errors.FirstOrDefault(),
                Success = false
            };

            return BadRequest(errorResponse);
        }
    }


    [HttpGet("GetValuesFromToken/{jwtToken}")]
    [Authorize]
    public GetTokenResult GetValuesFromToken(string jwtToken)
    {
        var result = new JwtTokenGenerator(_config).GetValuesFromToken(jwtToken);
        return result;
    }

    //HtppGet para convertir a base64 los atributos de CreateUserCommand (Name, Email, Password) para swagger o postman
    [HttpGet("convert-tobase64")]
    public string ConvertToBase64(string value)
    {
        var chain = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(chain);
    }
}
