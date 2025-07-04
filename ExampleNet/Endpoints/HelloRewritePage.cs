// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Html;
using Genova.Common.Localization;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping cache-related endpoints.
/// </summary>
internal static class HelloRewritePage
{
    /// <summary>
    /// Maps the cache-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the cache-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET /hello/rewrite
        endpoints.MapGet("/hello/rewrite", (HttpContext httpContext) =>
        {
            // Get the value of the "query" query string parameter
            string? queryValue = httpContext.Request.Query["query"].FirstOrDefault();

            // Generate HTML
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>This path has not been rewritten</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>This path has not been rewritten</h1>
                    <!-- This page should be viewed because there's no server-side
                         rewrite from /hello/rewrite to /hello/rewritten -->
                    <!-- ExampleNet HelloPages -->
                    <!-- Query Value: {queryValue} -->
                </body>
                </html>
                """).ToString();

            return Results.Content(html, "text/html; charset=utf-8");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
