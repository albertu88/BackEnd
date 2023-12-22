﻿using BackEnd.Infrastructure.Models.DTO;
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
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
                return BadRequest("Invalid client request");

            if (user.UserName == "albertico" && user.Password == "albertico")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySecretKey@12345"));
                var signCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>   {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, Role.Manager.ToString())
            };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7090",
                    audience: "https://localhost:7090",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }

            return Unauthorized();

        }
    }
}
