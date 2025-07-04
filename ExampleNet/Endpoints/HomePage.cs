// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Razor;
using Genova.Common.Websites;
using Genova.ExampleNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping the home page endpoint.
/// </summary>
internal static class HomePage
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the home page endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the home page endpoint.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (HttpContext httpContext) =>
        {
            Model model = new();

            string renderedHtml = await RazorViewRenderer.RenderViewAsync(
                httpContext,
                $"{Website.NamePrefix}_Home",
                model);

            return Results.Content(renderedHtml, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
