using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Text;
using System.Text.Json;
using Azure;

namespace Athena.WebApi.OutputCache
{
    //public class CustomOutputCacheFilter : IAsyncActionFilter
    //{
    //    //private readonly IOutputCacheStore _cacheStore;

    //    //public CustomOutputCacheFilter(IOutputCacheStore cacheStore)
    //    //{
    //    //    _cacheStore = cacheStore;
    //    //}

    //    //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    //{
    //    //    if (context.ActionDescriptor is ControllerActionDescriptor descriptor &&
    //    //        descriptor.MethodInfo.GetCustomAttributes(typeof(HttpGetAttribute), false).Length > 0)
    //    //    {
    //    //        var clientid = context.RouteData.Values["clientId"]?.ToString();
    //    //        var parameterName = string.Empty;
    //    //        var endpoint = context.HttpContext.GetEndpoint();
    //    //        var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

    //    //        if (actionDescriptor != null)
    //    //        {
    //    //            var attribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(DynamicOutputCacheTagAttribute), false)
    //    //                                                 .Cast<DynamicOutputCacheTagAttribute>()
    //    //                                                 .FirstOrDefault();
    //    //            if (attribute != null)
    //    //            {
    //    //                parameterName = attribute.ParameterName;
    //    //            }
    //    //        }


    //    //        if (!string.IsNullOrWhiteSpace(parameterName))
    //    //        {
    //    //            var key = $"{clientid}_{parameterName}";

    //    //            var cachedValue = await _cacheStore.GetAsync(key, context.HttpContext.RequestAborted);
    //    //            if (cachedValue != null)
    //    //            {
    //    //                var cachedObject = JsonSerializer.Deserialize<object>(cachedValue);
    //    //                context.Result = new JsonResult(cachedObject)
    //    //                {
    //    //                    ContentType = "application/json"
    //    //                };

    //    //                return;
    //    //            }

    //    //            var executedContext = await next();

    //    //            if (executedContext.Result is ObjectResult result)
    //    //            {
    //    //                var bodyBytes = JsonSerializer.SerializeToUtf8Bytes(result.Value);
    //    //                var tags = new[] { key };
    //    //                var validFor = TimeSpan.FromMinutes(10); // Set a desired cache duration

    //    //                await _cacheStore.SetAsync(key, bodyBytes, tags, validFor, context.HttpContext.RequestAborted);
    //    //            }
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        await next();
    //    //    }
    //    //}

    //    //public async Task EvictByTagFromContextAsync(ActionExecutingContext context, string tag)
    //    //{
    //    //    var clientid = context.RouteData.Values["clientId"]?.ToString();
    //    //    if (!string.IsNullOrEmpty(clientid))
    //    //    {
    //    //        var modifiedTag = $"{clientid}_{tag}";
    //    //        await _cacheStore.EvictByTagAsync(modifiedTag, context.HttpContext.RequestAborted);
    //    //    }
    //    //}
    //}
}
