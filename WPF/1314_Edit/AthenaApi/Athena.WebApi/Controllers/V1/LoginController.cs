using Asp.Versioning;
using Athena.Application.Interface;
using Athena.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public LoginController(ILogger<IngredientsController> logger, IUserService userService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Login/{clientId}")]
        [SwaggerOperation(Summary = "Login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel requestData)
        {
            try
            {
                if (requestData.Application == null || _appSettings.AlowedApplications == null || !_appSettings.AlowedApplications.Contains(requestData.Application.ToLower()))
                    return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized");

                var data = await _userService.Login(requestData.UserName, requestData.Password, requestData.Application);
                if (data == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Incorrect username or password, please try again");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
