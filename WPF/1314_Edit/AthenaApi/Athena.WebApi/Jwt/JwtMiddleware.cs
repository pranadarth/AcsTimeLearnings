using Athena.Application.Interface;
using System.Net;
using System.Web;

namespace Athena.WebApi.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IJwtTokenService _jwtTokenService;


        public JwtMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
        {
            _next = next;
            _jwtTokenService = jwtTokenService;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await ValidateToken(context, token);

            await _next(context);
        }

        private async Task ValidateToken(Microsoft.AspNetCore.Http.HttpContext context, string token)
        {
            string application = _jwtTokenService.ValidateToken(context, token);
            if (!string.IsNullOrEmpty(application))
            {
                context.Items["ApplicationName"] = application;
            }
        }
    }
}