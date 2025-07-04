// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping the robots.txt endpoint.
/// </summary>
/// <remarks>
/// For more information, see https://www.robotstxt.org.
/// </remarks>
internal static class RobotsFiles
{
    /// <summary>
    /// Maps the robots.txt endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the robots.txt endpoint.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/robots.txt", (HttpContext httpContext) =>
        {
            string domain = httpContext.Request.Host.ToString();

            string robotsTxtContent = $"""
                User-agent: *
                Disallow:

                Sitemap: https://{domain}/sitemap.xml
                """;

            return Results.Text(robotsTxtContent, "text/plain");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
