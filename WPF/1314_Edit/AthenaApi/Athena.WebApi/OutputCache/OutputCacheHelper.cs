using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System;

namespace Athena.WebApi.OutputCache
{
    public class OutputCacheHelper
    {
        public async static void DeleteOutputCache(HttpContext httpContext, IOutputCacheStore _cacheStore, string key, CancellationToken cancellationToken)
        {
            var tagValue = httpContext.Request.RouteValues["clientId"]?.ToString();
            if (!string.IsNullOrEmpty(tagValue))
            {
                var keyToDelete = $"{tagValue}_{key}";
                await _cacheStore.EvictByTagAsync(keyToDelete, cancellationToken);
            }
        }
    }
}
