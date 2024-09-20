using Asp.Versioning;
using Athena.Application.Interface;
using Athena.Domain.Models;
using Athena.WebApi.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Tags("Ingredient Caloric")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class IngredientCalorieController : ControllerBase
    {

        private readonly ILogger<IngredientsController> _logger;
        private readonly IIngredientCalorieService _ingredientCalorieService;

        public IngredientCalorieController(ILogger<IngredientsController> logger, IIngredientCalorieService ingredientCalorieService)
        {
            _logger = logger;
            _ingredientCalorieService = ingredientCalorieService;
        }


        [HttpGet("{clientId}/{ingSk}")]
        [SwaggerOperation(Summary = "Get Ingredient Caloric Information")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long ingSk)
        {
            try
            {
                var data = await _ingredientCalorieService.Get(ingSk);
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
        [SwaggerOperation(Summary = "Save Ingredient Caloric Information")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Save([FromBody] List<SaveIngredientCaloricReqModel> reqData)
        {
            try
            {
                if (reqData == null || reqData.Count < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Request body cannot be empty");

                long ingSk = reqData.Select(i => i.IngSk).FirstOrDefault();
                if (ingSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredient not available");

                var data = await _ingredientCalorieService.Save(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Update Ingredient Caloric Information")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] List<UpdateIngredientCaloricInfoReqModel> reqData)
        {
            try
            {
                if (reqData == null || reqData.Count < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Request body cannot be empty");

                long ingSk = reqData.Select(i => i.IngSk).FirstOrDefault();
                if (ingSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredient not available");

                var data = await _ingredientCalorieService.Update(reqData);
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
