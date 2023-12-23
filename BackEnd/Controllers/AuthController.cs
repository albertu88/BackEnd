using BackEnd.Applicaton.Services.Contracts;
using BackEnd.Infrastructure.Models.DTO;
using BackEnd.Infrastructure.Models.Enum;
using BackEnd.Infrastructure.Models.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly ITokenService _tokenService;

        public AuthController(UserContext userContext, ITokenService tokenService)
        {
            _userContext = userContext;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
                return BadRequest("Invalid client request");


            var user = _userContext.LoginModels?.Where(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password).FirstOrDefault();

            if (user is null)
                return Unauthorized();

            var claims = new List<Claim>   {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                new Claim(ClaimTypes.Role, Role.Manager.ToString())
                };


            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime =DateTime.UtcNow.AddDays(7);

            _userContext.SaveChanges();

                return Ok(new AuthenticatedResponse 
                { 
                    Token = accessToken,
                    RefreshToken = refreshToken
                
                });
                                 

        }
    }
}
