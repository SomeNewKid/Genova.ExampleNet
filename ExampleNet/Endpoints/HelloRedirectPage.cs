// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Html;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping cache-related endpoints.
/// </summary>
internal static class HelloRedirectPage
{
    /// <summary>
    /// Maps the cache-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the cache-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET /hello/redirect
        endpoints.MapGet("/hello/redirect", (HttpContext httpContext) =>
        {
            // Get the value of the "query" query string parameter
            string? queryValue = httpContext.Request.Query["query"].FirstOrDefault();

            // Generate HTML
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>This path has not been redirected</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>This path has not been redirected</h1>
                    <!-- This page should be viewed because there's no client-side redirection
                         from /hello/redirect to /hello/redirected -->
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
