using BackEnd.Applicaton.Services.Contracts;
using BackEnd.Infrastructure.Models.DTO;
using BackEnd.Infrastructure.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly UserContext _userContext;
        private readonly ITokenService _tokenService;

        public TokenController(UserContext userContext, ITokenService tokenService)
        {
            _userContext = userContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal =_tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;

            var user = _userContext.LoginModels.SingleOrDefault(u => u.UserName == userName);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest();

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new AuthenticatedResponse() {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke() 
        {
            var username = User.Identity.Name;

            var user = _userContext.LoginModels.SingleOrDefault(u => u.UserName == username);

            if (user is null)
                return BadRequest();

            user.RefreshToken = null;

            _userContext.SaveChanges();

            return NoContent();

        }
    }
}
