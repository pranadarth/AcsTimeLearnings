using Asp.Versioning;
using Athena.Application.Interface;
using Athena.Application.Service;
using Athena.Domain.Common;
using Athena.Domain.Models;
using Athena.WebApi.Jwt;
using Athena.WebApi.OutputCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Tags("Ingredient Allergens")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    public class IngredientAllergensController : ControllerBase
    {
        private readonly ILogger<IngredientAllergensController> _logger;
        private readonly IIngredientAllergenService _iingredientAllergenService;

        public IngredientAllergensController(ILogger<IngredientAllergensController> logger, IIngredientAllergenService iingredientAllergenService)
        {
            _logger = logger;
            _iingredientAllergenService = iingredientAllergenService;
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Allergen Options")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_AllergenOptions)]

        public async Task<IActionResult> GetAllergenOptions()
        {
            try
            {
                var data = await _iingredientAllergenService.GetAllergenOptions();
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("{clientId}/{ingSk}")]
        [SwaggerOperation(Summary = "Get Ingredient Allergens")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Allergens)]
        public async Task<IActionResult> GetAllergens(long ingSk)
        {
            try
            {
                var data = await _iingredientAllergenService.Get(ingSk);
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost("{clientId}")]
        [SwaggerOperation(Summary = "Save Ingredient Allergens")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Save([FromBody] List<SaveIngredientAllergenRequestModel> reqData)
        {
            try
            {
                if (reqData == null || reqData.Count < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Request body cannot be empty");

                long ingSk = reqData.Select(i => i.IngSk).FirstOrDefault();
                if (ingSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredient not available");

                var data = await _iingredientAllergenService.Save(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Update Ingredient Allergens")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] List<SaveIngredientAllergenRequestModel> reqData)
        {
            try
            {
                if (reqData == null || reqData.Count < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Request body cannot be empty");

                long ingSk = reqData.Select(i => i.IngSk).FirstOrDefault();
                if (ingSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredient not available");

                var data = await _iingredientAllergenService.Save(reqData);
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
