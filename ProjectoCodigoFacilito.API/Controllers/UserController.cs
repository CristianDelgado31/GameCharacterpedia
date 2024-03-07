using Azure.Core;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectoCodigoFacilito.Application.CheckUser;
using ProjectoCodigoFacilito.Application.Common.Exceptions;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Commands.DeleteUser;
using ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserById;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUserSignIn;
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
    public async Task<ActionResult<UserResult>> CheckUser(CheckUserQuery checkUser)
    {   
        var userDto = await Mediator.Send(checkUser);

        if (userDto == null)
            return NotFound();

        var token = GenerateJwt(userDto);
        
        return Ok(new UserResult
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


    private string GenerateJwt(UserDTO user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Crear los claims
        var claims = new[]
        {
             new Claim(JwtClaimTypes.Id, user.Id.ToString()),
             new Claim(JwtClaimTypes.Role, user.Role)
         };

        // Crear el token

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpGet("GetTokenId/{jwtToken}")]
    [Authorize]
    public GetTokenResult GetUserIdFromToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);

        var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);
        if (userIdClaim == null)
        {
            return null;
        }

        // Leer la fecha de expiración del token en formato UTC
        var expirationTimeUtc = token.ValidTo;

        // Obtener información de la zona horaria UTC-3
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");


        // Convertir la fecha y hora de expiración a la zona horaria UTC-3
        var expirationTimeUtcMinus3 = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, timeZone);

        // Retornar el resultado con la fecha y hora en la zona horaria UTC-3
        return new GetTokenResult { Id = userIdClaim.Value, ExpirationToken = expirationTimeUtcMinus3 };
    }

    // clase de prueba para la funcion de arriba
    public class GetTokenResult
    {
        public string Id { get; set; }
        public DateTime ExpirationToken { get; set; }

    }
}
