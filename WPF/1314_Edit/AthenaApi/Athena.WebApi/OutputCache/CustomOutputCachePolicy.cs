using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;

namespace Athena.WebApi.OutputCache
{
    public class DynamicTagOutputCachePolicy : IOutputCachePolicy
    {
        public async ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (descriptor != null)
            {
                var attribute = descriptor.MethodInfo.GetCustomAttributes(typeof(DynamicOutputCacheTagAttribute), false)
                                                     .Cast<DynamicOutputCacheTagAttribute>()
                                                     .FirstOrDefault();
                if (attribute != null)
                {
                    var tagValue = context.HttpContext.Request.RouteValues["clientId"]?.ToString();
                    if (!string.IsNullOrEmpty(tagValue))
                    {
                        var key = $"{tagValue}_{attribute.ParameterName}";
                        context.Tags.Add(key);
                    }
                }
            }
            await Task.CompletedTask;
        }

        public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken) => ValueTask.CompletedTask;

        public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken) => ValueTask.CompletedTask;
    }
}