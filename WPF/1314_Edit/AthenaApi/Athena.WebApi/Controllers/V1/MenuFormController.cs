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
    [Tags("Menu Form")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    public class MenuFormController : ControllerBase
    {
        private readonly ILogger<MenuFormController> _logger;
        private readonly IMenuFormSevice _iMenuFormService;
        private readonly IOutputCacheStore _outputCacheStore;

        public MenuFormController(ILogger<MenuFormController> logger, IDishService iDishService, IMenuFormSevice iMenuFormService, IOutputCacheStore outputCacheStore)
        {
            _logger = logger;
            _iMenuFormService = iMenuFormService;
            _outputCacheStore = outputCacheStore;
        }


        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Menu Form Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_MenuFormTypes)]
        public async Task<IActionResult> GetMenuFormTypes()
        {
            try
            {
                var data = await _iMenuFormService.GetMenuFormTypes();
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
        [SwaggerOperation(Summary = "Get Locations")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Locations)]
        public async Task<IActionResult> GetLocations(string? location = null)
        {
            try
            {
                var data = await _iMenuFormService.GetLocations(location);
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

        [HttpGet("{clientId}/{locationSk}")]
        [SwaggerOperation(Summary = "Get Sub Locations")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_SubLocations)]
        public async Task<IActionResult> GetSubLocations(int locationSk)
        {
            try
            {
                var data = await _iMenuFormService.GetSubLocations(locationSk);
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


        [HttpGet("{clientId}/{locationSk}")]
        [SwaggerOperation(Summary = "Get All Location and Sub Locations")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_AllLocations)]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var data = await _iMenuFormService.GetAllLocations();
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
        [SwaggerOperation(Summary = "Get Cut off Timings")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_SubLocations)]
        public async Task<IActionResult> GetCutOffTimings()
        {
            try
            {
                var data = await _iMenuFormService.GetCutOffTimings();
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

        [HttpGet("{clientId}/{locationSk}/{startDate}/{endDate}")]
        [SwaggerOperation(Summary = "Get Menu Planner Dashboard data")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMenuPlannerDashBoardData(int menuFormTypeSk, int locationSk, DateTime startDate, DateTime endDate, int? subLocationSk = null)
        {
            try
            {
                var data = await _iMenuFormService.GetMenuPlannerDashBoardData(menuFormTypeSk, locationSk, subLocationSk, startDate, endDate);
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
        [SwaggerOperation(Summary = "Copy Menu")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CopyMenu([FromBody] CopyMenuReqModel reqData)
        {
            try
            {
                var data = await _iMenuFormService.CopyMenu(reqData);
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
        [SwaggerOperation(Summary = "Add Menu Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMenuDishes([FromBody] AddMenuFormDishesReqModel reqData)
        {
            try
            {
                var data = await _iMenuFormService.AddMenuDishes(reqData);
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

        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Update Menu Dishes")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMenuDishes([FromBody] UpdateMenuFormDishesReqModel reqData)
        {
            try
            {
                var data = await _iMenuFormService.UpdateMenuDishes(reqData);
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
        [SwaggerOperation(Summary = "Prefill Saved Data")]
        [ProducesResponseType(typeof(MenuFormDishesModel), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuFormDishes([FromBody] GetMenuFormDishesReqModel reqModel)
        {
            try
            {
                var data = await _iMenuFormService.GetMenuFormDishes(reqModel);
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
    }
}
