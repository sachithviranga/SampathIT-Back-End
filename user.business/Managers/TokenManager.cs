using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using user.contracts.Managers;
using user.entities.Common.Configuration;
using user.entities.DTO;
using user.entities.Entities;

namespace user.business.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly IOptions<AuthenticationConfiguration> _authenticationConfigurations;

        protected readonly UserManager<ApplicationUser> _identityUserManager;


        public TokenManager(IOptions<AuthenticationConfiguration> authenticationConfigurations
            , UserManager<ApplicationUser> identityUserManager)
        {
            _authenticationConfigurations = authenticationConfigurations;
            _identityUserManager = identityUserManager;
        }

        public string GenerateAccessToken(UserDTO user)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfigurations.Value.jwt.Secret));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expiryTime = _authenticationConfigurations.Value.jwt.Expires;

                var tokenOptions = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new List<Claim>
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                }),
                    Expires = DateTime.Now.AddMinutes(expiryTime),
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenOptions);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GenerateRefreshToken(ApplicationUser applicationUser)
        {
            try
            {
                await _identityUserManager.RemoveAuthenticationTokenAsync(applicationUser, "User", "RefreshToken");
                var refreshToken = await _identityUserManager.GenerateUserTokenAsync(applicationUser, "User", "RefreshToken");
                await _identityUserManager.SetAuthenticationTokenAsync(applicationUser, "User", "RefreshToken", refreshToken);

                return refreshToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ValidateRefreshToken(string token, string userId)
        {
            try
            {
                var user = await _identityUserManager.FindByIdAsync(userId);

                var refreshToken = await _identityUserManager.GetAuthenticationTokenAsync(user, "User", "RefreshToken");
                var isValid = await _identityUserManager.VerifyUserTokenAsync(user, "User", "RefreshToken", token);

                return isValid;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ValidateToken(string token)
        {
            try
            {
                if (token == null)
                    return false;

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfigurations.Value.jwt.Secret))
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
