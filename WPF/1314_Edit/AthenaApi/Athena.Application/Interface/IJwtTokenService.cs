using Athena.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IJwtTokenService
    {
        public Task<JWTTokenModel> GetJwtToken(string applicationName);
        public string ValidateToken(HttpContext context, string token);
    }
}
