﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RailWorkshop.API.Utils;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : GeneralController
    {
        private readonly JWTSettings _options;
        private readonly IEmployeeRepository _employeeRepository;

        public AuthenticationController(IOptions<JWTSettings> optionsAccess, IEmployeeRepository employeeRepository)
        {
            _options = optionsAccess.Value;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{login}/{password}")]
        public async Task<IActionResult> GetToken(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (login == _options.AdminLogin && password == _options.AdminPassword)
            {
                List<Claim> claims = new()
                {
                    new(ClaimTypes.Role, "Admin")
                };

                return Ok(GenerateToken(claims));
            }

            try
            {
                Employee employee = await _employeeRepository.GetByLoginAndPassword(login, password);

                List<Claim> claims = new()
                {
                    new(ClaimTypes.Role, "Employee"),
                    new("id", employee.Id.ToString())
                };

                return Ok(GenerateToken(claims));
            }
            catch (IncorrectLoginOrPasswordException)
            {
                return Forbid();
            }
        }

        public string GenerateToken(IList<Claim> claims)
        {
            SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(_options.SecretKey));

            JwtSecurityToken token =
                new(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}