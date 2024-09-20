using Asp.Versioning;
using Athena.Application.Interface;
using Athena.Application.Service;
using Athena.Domain.Common;
using Athena.Domain.Models;
using Athena.WebApi.Jwt;
using Athena.WebApi.OutputCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athena.WebApi.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Tags("Dish")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly IDishService _iDishService;
        private readonly IOutputCacheStore _outputCacheStore;

        public DishController(ILogger<IngredientsController> logger, IDishService iDishService, IOutputCacheStore outputCacheStore)
        {
            _logger = logger;
            _iDishService = iDishService;
            _outputCacheStore = outputCacheStore;
        }



        [HttpGet("{clientId}/{recsPerPage}/{currPageNo}")]
        [SwaggerOperation(Summary = "Get Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int recsPerPage, int currPageNo)
        {
            try
            {
                var data = await _iDishService.GetDishes(recsPerPage, currPageNo);
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDish(int dishSk)
        {
            try
            {
                var data = await _iDishService.GetDishDetails(dishSk);
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


        [HttpGet("{clientId}/{recsPerPage}/{currPageNo}")]
        [SwaggerOperation(Summary = "Search Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(int recsPerPage, int currPageNo, string dishName = null, int? dishCategoryId = null, float? portionSize = null, float? costPerPortion = null, long? ingSk = null, string exactDishName = null)
        {
            try
            {
                var data = await _iDishService.Search(dishName, dishCategoryId, portionSize, costPerPortion, recsPerPage, currPageNo, ingSk, exactDishName);
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Meal Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_MealTypes)]
        public async Task<IActionResult> GetMealTypes()
        {
            try
            {
                var data = await _iDishService.GetMealTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Menu Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_MenuType)]
        public async Task<IActionResult> GetMenuTypes(int? locationId = null, int? subLocationId = null)
        {
            try
            {
                var data = await _iDishService.GetMenuTypes(locationId, subLocationId);
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Food Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_FoodType)]
        public async Task<IActionResult> GetFoodTypes()
        {
            try
            {
                var data = await _iDishService.GetFoodTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Label Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_LabelTypes)]
        public async Task<IActionResult> GetLabelTypes()
        {
            try
            {
                var data = await _iDishService.GetLabelTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Templates")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Templates)]
        public async Task<IActionResult> GetTemplates()
        {
            try
            {
                var data = await _iDishService.GetTemplates();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Portion Control")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_PortionControl)]
        public async Task<IActionResult> GetPortionControl()
        {
            try
            {
                var data = await _iDishService.GetPortionControl();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Spice Level")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_SpiceLevel)]
        public async Task<IActionResult> GetSpiceLevel()
        {
            try
            {
                var data = await _iDishService.GetSpiceLevel();
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
        [SwaggerOperation(Summary = "Export Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExportDishes([FromBody] GetExportDishesReqModel reqData)
        {
            try
            {
                var data = await _iDishService.GetExportDishes(reqData);
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
        [SwaggerOperation(Summary = "Filter Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilterDishes([FromBody] GetFilterDishesReqModel reqData)
        {
            try
            {
                var data = await _iDishService.GetFilterDishes(reqData);
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Dropdowns")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_DishDropDowns)]
        public async Task<IActionResult> GetDishDropDowns()
        {
            try
            {
                var data = await _iDishService.GetDishDropDowns();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Categories")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_DishCategories)]
        public async Task<IActionResult> GetDishCategories()
        {
            try
            {
                var data = await _iDishService.GetDishCategories();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Preparation Process Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_PreparationProcessTypes)]
        public async Task<IActionResult> GetPreparationProcessTypes()
        {
            try
            {
                var data = await _iDishService.GetPreparationProcessTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Preparation Process Steps")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_PreparationProcessSteps)]
        public async Task<IActionResult> GetPreparationProcessSteps()
        {
            try
            {
                var data = await _iDishService.GetPreparationProcessSteps();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Process Selection Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_ProcessSelectionTypes)]
        public async Task<IActionResult> GetProcessSelectionTypes()
        {
            try
            {
                var data = await _iDishService.GetProcessSelectionTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Timing Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_DishTimingTypes)]
        public async Task<IActionResult> GetDishTimingTypes()
        {
            try
            {
                var data = await _iDishService.GetDishTimingTypes();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Temperature Units")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_DishTemperatureUnits)]
        public async Task<IActionResult> GetDishTemperatureUnits(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _iDishService.GetDishTemperatureUnits();
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


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Dish Preparation Dropdowns")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_DishPreparationDropdowns)]
        public async Task<IActionResult> GetDishPreparationDropdowns()
        {
            try
            {
                var data = await _iDishService.GetDishPreparationDropdowns();
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
        [SwaggerOperation(Summary = "Save Dish")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveDish(SaveDishDetailsReqModel reqData)
        {
            try
            {
                if (reqData.DishGenerationInfo == null || string.IsNullOrEmpty(reqData.DishGenerationInfo.DishName))
                    return NotFound("Dish general information required");

                if (reqData.DishIngredients == null || reqData.DishIngredients.Count < 1)
                    return NotFound("Dish ingredients required");


                bool isDishNameExist = await _iDishService.IsDishNameExists(reqData.DishGenerationInfo.DishName);
                if (isDishNameExist)
                    return Conflict("Dish Name already exists");

                var data = await _iDishService.SaveDish(reqData);
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
        [SwaggerOperation(Summary = "Upload Dish Image")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadDishImage(string clientId, [FromForm] DishUploadReqModel reqData)
        {
            try
            {
                if (reqData.Image == null || reqData.Image.Length == 0)
                    return BadRequest("Image file is required.");

                var data = await _iDishService.UploadDishImage(clientId, reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Update Dish")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDish(UpdateDishDetailsReqModel reqData)
        {
            try
            {
                if (reqData.DishGenerationInfo == null || string.IsNullOrEmpty(reqData.DishGenerationInfo.DishName))
                    return NotFound("Dish general information required");

                if (reqData.DishIngredients == null || reqData.DishIngredients.Count < 1)
                    return NotFound("Dish ingredients required");

                int dishSk = await _iDishService.GetDishSkByName(reqData.DishGenerationInfo.DishName);
                if (dishSk != reqData.DishGenerationInfo.DishSk)
                    return Conflict("Dish Name already exists");

                var data = await _iDishService.UpdateDish(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{clientId}")]
        [SwaggerOperation(Summary = "Save Dish Ingredient Substitution")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveDishIngredientSubstitution([FromBody] SaveDishIngSubstitutionReqModel reqData)
        {
            try
            {
                if (reqData.PreIngSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredients Sk Required");

                if (reqData.SubstituteWithIngSk < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Substitute With is Empty");

                if (reqData.SubstituteOnDish == null || reqData.SubstituteOnDish.Count < 1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Substitute on Dish is Empty");

                var data = await _iDishService.SaveDishIngSubstitution(reqData);
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
