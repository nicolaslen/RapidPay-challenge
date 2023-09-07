using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation.Results;
using RapidPay.Application.Exceptions;
using RapidPay.Application.Services.Cards.Validators;
using RapidPay.Infrastructure.Authentication.Validators;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RapidPay.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }
        public async Task<RegistrationResponseDto> Registration(RegistrationDto model)
        {
            var validator = new RegisterUserValidator();
            ValidationResult validationResult = await validator.ValidateAsync(model);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            User userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new RegistrationResponseDto("User already exists");

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return new RegistrationResponseDto("User creation failed! Please check user details and try again.");

            return new RegistrationResponseDto();
        }

        public async Task<LoginResponseDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new LoginResponseDto(false, "Invalid login");

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            string token = GenerateToken(authClaims);
            return new LoginResponseDto(token);
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var tokenExpiryTimeInHour = Convert.ToInt32(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
