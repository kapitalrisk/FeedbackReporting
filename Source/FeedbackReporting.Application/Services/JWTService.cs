using FeedbackReporting.Domain.Constants;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.Services;
using InMemoryDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepo;

        public JWTService(IConfiguration configuration, IUserRepository userRepo)
        {
            _configuration = configuration;
            _userRepo = userRepo;
        }

        public string GenerateJWT(UserRessource user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                 new Claim(JwtRegisteredClaimNames.Email, user.Name),
                 new Claim(Claims.UserRoles, user.Role),
                 new Claim(Claims.ClaimCreationDate, DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_configuration["JwtAuth:Issuer"],
              _configuration["JwtAuth:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }

        public async Task<UserRessource> LoginUser(LoginRessource loginIdentifiers)
        {
            var user = await _userRepo.GetByName(loginIdentifiers.UserName);

            if (user != null && PasswordHashHandler.VerifyPasswordHash(loginIdentifiers.Password, user.PasswordHash))
                return new UserRessource(user);
            else
                return null;
        }

        public async Task<bool> CreateUser(UserRessource user)
        {
            // Generate passwordhash
            var entityToInsert = user.ToEntity();
            entityToInsert.PasswordHash = PasswordHashHandler.GetPasswordHashForStorage(user.Password);
            await _userRepo.Insert(entityToInsert);
            return true;
        }

        public async Task<bool> DeleteUserByName(string name)
        {
            return await _userRepo.DeleteByName(name);
        }
    }
}
