// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Razor;
using Genova.Common.Websites;
using Genova.ExampleNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping the naughty page endpoint.
/// </summary>
internal static class NaughtyPage
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the naughty page endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the naughty page endpoint.</param>
    /// <remarks>
    /// This method will not complete because the attempt to use the view of another website will result
    /// in an <see cref="InvalidOperationException"/>. Hence, the method is excluded from code coverage.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/naughty", async (HttpContext httpContext) =>
        {
            Model model = new();

            string renderedHtml = await RazorViewRenderer.RenderViewAsync(
                httpContext,
                "EXAMPLECOM_Home", // use view of another website
                model);

            return Results.Content(renderedHtml, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
