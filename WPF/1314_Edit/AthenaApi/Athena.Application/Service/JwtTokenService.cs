using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Athena.Domain.Models;

namespace Athena.Application.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly AppSettings _appSettings;
        public JwtTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<JWTTokenModel> GetJwtToken(string applicationName)
        {
            if (applicationName == null || _appSettings.AlowedApplications == null || !_appSettings.AlowedApplications.Contains(applicationName.ToLower()))
            {
                return null;
            }

            JWTTokenModel jwtTokenModel = new JWTTokenModel();

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("application", applicationName) }),
                    Expires = DateTime.UtcNow.AddDays(_appSettings.JwtExpiryInHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });
            jwtTokenModel.Token = tokenHandler.WriteToken(token);
            jwtTokenModel.ExpiryInHours = _appSettings.JwtExpiryInHours;
            jwtTokenModel.GeneratedDateTime = DateTime.UtcNow;

            return jwtTokenModel;
        }

        public string ValidateToken(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var applicationName = jwtToken.Claims.First(x => x.Type == "application").Value;

                if (applicationName == null || _appSettings.AlowedApplications == null || !_appSettings.AlowedApplications.Contains(applicationName.ToLower()))
                {
                    throw new UnauthorizedAccessException();
                }
                return applicationName.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
