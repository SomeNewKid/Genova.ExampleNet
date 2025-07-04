// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Html;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping cache-related endpoints.
/// </summary>
internal static class HelloCachePages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the cache-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the cache-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET /hello/cache
        endpoints.MapGet("/hello/cache", (HttpContext httpContext) =>
        {
            string html = GenerateSampleHtml("Hello, Cache", "This page should be cached.", "Banana and tomato");
            return Results.Content(html, HtmlContentType);
        })
        .CacheOutput(Website.CachePolicyName).WithTags(Website.CachePolicyTag)
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        // GET /hello/dont-cache
        endpoints.MapGet("/hello/dont-cache", (HttpContext httpContext) =>
        {
            string html = GenerateSampleHtml("Hello, Don’t Cache", "This page should not be cached.", "Banana and tomato");
            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        // GET /hello/purge-cache
        endpoints.MapGet("/hello/purge-cache", async (HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            IOutputCacheStore outputCacheStore = httpContext.RequestServices.GetRequiredService<IOutputCacheStore>();
            await outputCacheStore.EvictByTagAsync(Website.CachePolicyTag, cancellationToken);
            string html = GenerateSampleHtml("Hello, Purge Cache", "This cache has been purged.", "Banana and tomato");
            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    private static string GenerateSampleHtml(string title, string message, string fruits)
    {
        return new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>{title}</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>{title}</h1>
                    <p>{message}</p>
                    <p>{fruits}</p>
                    <!-- ExampleNet HelloPages -->
                </body>
                </html>
                """).ToString();
    }
}
