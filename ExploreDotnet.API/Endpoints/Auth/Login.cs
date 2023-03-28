using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ExploreDotnet.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExploreDotnet.API.Endpoints.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    
    public class Login : EndpointBaseAsync.WithRequest<LoginDto>.WithActionResult
    {
        private readonly DatabaseContext _context;

        public Login(DatabaseContext context)
        {
            _context = context;
        }
        
        [HttpPost("api/auth/login")]
        public override async Task<ActionResult> HandleAsync(LoginDto request, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.Where(e => e.Email == request.Email).FirstOrDefaultAsync(cancellationToken);

            if (user is null || user.Password != request.Password)
            {
                return BadRequest("Invalid email password combination.");
            }
            
            var claims = new List<Claim>
            {
                new ("userId", user.Id.ToString(), ClaimValueTypes.Integer64),
            };
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kosong0987654321"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                claims,
                expires: DateTime.Now.AddMinutes(12700),
                signingCredentials: signinCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(token);
        }
    }
}