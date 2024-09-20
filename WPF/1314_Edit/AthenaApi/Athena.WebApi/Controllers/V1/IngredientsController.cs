using Asp.Versioning;
using Athena.Application.Interface;
using Athena.Application.Interface.Reports;
using Athena.Application.Service.Reports;
using Athena.Domain.Common;
using Athena.Domain.Models;
using Athena.WebApi.Jwt;
using Athena.WebApi.OutputCache;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Athena.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Tags("Ingredients")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly IIngredientsService _ingredientsService;
        private readonly IIngredientStoreService _ingredientStoreService;

        public IngredientsController(ILogger<IngredientsController> logger, IIngredientsService ingredientsService, IIngredientStoreService ingredientStoreService)
        {
            _logger = logger;
            _ingredientsService = ingredientsService;
            _ingredientStoreService = ingredientStoreService;
        }

        [Authorize]
        [HttpGet("{clientId}/{recsPerPage}/{currPageNo}")]
        [SwaggerOperation(Summary = "Get ingredients")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int recsPerPage, int currPageNo, string supplierName = null)
        {
            try
            {
                var data = await _ingredientsService.GetIngredients(supplierName, recsPerPage, currPageNo);
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

        [Authorize]
        [HttpPost("{clientId}/{recsPerPage}/{currPageNo}")]
        [SwaggerOperation(Summary = "Search Ingredients")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(int recsPerPage, int currPageNo, string searchText = null, string supplierName = null, float? cost = null, string supplierReferenceNo = null, [FromBody] IngredientSearchRequestModel reqData = null, string ingredientName = null)
        {
            try
            {
                var data = await _ingredientsService.SearchIngredients(supplierName, searchText, cost, supplierReferenceNo, reqData, recsPerPage, currPageNo, ingredientName);
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Pack Unit of Measure")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_PackUOM)]
        public async Task<IActionResult> GetPackUOM()
        {
            try
            {
                var data = await _ingredientsService.GetPackUOM();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get stores")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Stores)]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var data = await _ingredientStoreService.GetStores();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get food groups")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Foodgroups)]
        public async Task<IActionResult> GetFoodgroups()
        {
            try
            {
                var data = await _ingredientsService.GetFoodgroups();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Suppliers")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Suppliers)]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var data = await _ingredientsService.GetSuppliers();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Countries List")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Countries)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var data = await _ingredientsService.GetCountries();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_Status)]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var data = await _ingredientsService.GetStatus();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get contracted status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_ContractedStatus)]
        public async Task<IActionResult> GetContractedStatus()
        {
            try
            {
                var data = await _ingredientsService.GetContractedStatus();
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

        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Ingredient Types")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OutputCache(PolicyName = "CustomOutputCacheTagPolicy")]
        [DynamicOutputCacheTag(Constants.OutputCache_Tag_IngredientTypes)]
        public async Task<IActionResult> GetIngredientTypes()
        {
            try
            {
                var data = await _ingredientsService.GetIngredientTypes();
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

        [Authorize]
        [HttpPost("{clientId}")]
        [SwaggerOperation(Summary = "Export Ingredients")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExportIngredients([FromBody] GetExportIngredientsReqModel reqData)
        {
            try
            {
                var data = await _ingredientsService.GetExportIngredients(reqData);
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

        [Authorize]
        [HttpPost("{clientId}")]
        [SwaggerOperation(Summary = "Apply Filter Ingredients")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyFilterIngredients([FromBody] ApplyFilterIngredientsReqModel reqData)
        {
            try
            {
                var data = await _ingredientsService.ApplyFilterIngredients(reqData);
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

        [Authorize]
        [HttpPost("{clientId}")]
        [SwaggerOperation(Summary = "Save Ingredient Details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Save([FromBody] SaveIngredientReqModel reqData)
        {
            try
            {
                if (string.IsNullOrEmpty(reqData.IngredientsName))
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredients Name Required");

                if (reqData.SalePrice < 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Cost Required");

                if (reqData.Weight < 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Weight Required");

                if (string.IsNullOrEmpty(reqData.PackSize))
                    return StatusCode(StatusCodes.Status400BadRequest, "Pack size is required");

                if (string.IsNullOrEmpty(reqData.SupplierReferenceNo))
                    return StatusCode(StatusCodes.Status400BadRequest, "Product code is required");


                if (string.IsNullOrEmpty(reqData.SupplierName))
                    return StatusCode(StatusCodes.Status400BadRequest, "Supplier is required");
                var ingNameProductCodeAndSupplier = await _ingredientsService.GetByIngNameProductCodeAndSupplier(reqData.IngredientsName, reqData.SupplierReferenceNo, reqData.SupplierName);

                if (ingNameProductCodeAndSupplier != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredients Name already exists.");
                }

                var ingredientPerProductCodeSupplier = await _ingredientsService.GetByProductCodeAndSupplier(reqData.SupplierReferenceNo.Trim(), reqData.SupplierName);
                if (ingredientPerProductCodeSupplier != null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Product code already exist for this provider.");

                if (string.IsNullOrEmpty(reqData.StoreSk.ToString()) || reqData.StoreSk == 0)
                {
                    var generalStoreId = await _ingredientStoreService.GetByCode("General");
                    reqData.StoreSk = generalStoreId.StoreSk;
                }

                var data = await _ingredientsService.Save(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Edit Ingredient Details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit([FromBody] EditIngredientReqModel reqData)
        {
            try
            {
                if (reqData.IngSK <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Ingredient sk Required");

                var ingredientPerProductCodeSupplier = await _ingredientsService.GetByProductCodeAndSupplier(reqData.SupplierReferenceNo.Trim(), reqData.PreferredSupplier);
                if (ingredientPerProductCodeSupplier != null && ingredientPerProductCodeSupplier.IngSk != reqData.IngSK)
                    return StatusCode(StatusCodes.Status400BadRequest, "Product code already exist for this provider.");

                var data = await _ingredientsService.Edit(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Save Ingredient Linking")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveIngredientLinking([FromBody] SaveIngredientLinkingReqModel reqData)
        {
            try
            {
                var data = await _ingredientsService.SaveIngredientLinking(reqData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Update Ingredient Generic Yield")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGenericYield([FromBody] UpdateGenericYieldReqModel reqData)
        {
            try
            {
                var data = await _ingredientsService.UpdateGenericYield(reqData.IngSk, reqData.GenericYield, reqData.UserId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


        [Authorize]
        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Get Ingredient Linkings")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIngredientLinkings(int ingSk)
        {
            try
            {
                var data = await _ingredientsService.GetIngredientLinkings(ingSk);
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

        #region Reports
        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Declaration Header details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<object> GetDeclarationHeaderDetails(string clientId)
        {

            return await _ingredientsService.GetDeclarationHeaderDetails();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Declaration details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetDeclarationDetails(string clientId, int supplierId, string status)
        {
            return await _ingredientsService.GetDeclarationDetails(supplierId, status);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Nutritional Header details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetNutritionalStatusHeaderDetails(string clientId)
        {

            return await _ingredientsService.GetNutritionalHeaderDetails();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Nutritional details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetNutritionalStatusDetails(string clientId, int supplierId, string status)
        {
            return await _ingredientsService.GetNutritionalDetails(supplierId, status);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Nutritional details without grouping")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetNutritionalStatusDetailsWithoutGrouping(string clientId, int supplierId, string status)
        {
            return await _ingredientsService.GetNutritionalDetailsWithoutGrouping(supplierId, status);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Allergen Header details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetAllergenStatusHeaderDetails(string clientId)
        {

            return await _ingredientsService.GetAllergenHeaderDetails();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Report - Get Allergen details")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<object> GetAllergenStausDetails(string clientId, int supplierId, string status)
        {
            return await _ingredientsService.GetAllergenDetails(supplierId, status);
        }
        #endregion
    }
}
