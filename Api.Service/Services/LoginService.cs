using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Repository;
using Api.Domain.DTOs;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Api.Domain.Entities;
using System.Security.Claims;
using System.Security.Principal;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        private string CreateToken(ClaimsIdentity identity, DateTime creationDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime creationDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                name = user.Name,
                message = "User login successful"
            };
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            UserEntity baseUser = new UserEntity();

            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                baseUser = await _repository.FindByEmail(user.Email);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Login failed! Not possible to auth user"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email),
                        }
                    );

                    DateTime creationDate = DateTime.Now;
                    DateTime expirationDate = creationDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, creationDate, expirationDate, handler);
                    return SuccessObject(creationDate, expirationDate, token, baseUser);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Login failed! Not possible to auth user"
                };
            }
        }
    }
}