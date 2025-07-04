// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Html;
using Genova.Common.Razor;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping friendly error pages.
/// </summary>
internal static class HelloFriendlyErrorPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the friendly error pages to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the friendly error pages.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello/friendly-400", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status400BadRequest,
                "ENGINE_ERROR_BadRequest_Heading",
                "ENGINE_ERROR_BadRequest_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-401", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status401Unauthorized,
                "ENGINE_ERROR_Unauthorized_Heading",
                "ENGINE_ERROR_Unauthorized_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-402", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status402PaymentRequired,
                "ENGINE_ERROR_PaymentRequired_Heading",
                "ENGINE_ERROR_PaymentRequired_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-403", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status403Forbidden,
                "ENGINE_ERROR_Forbidden_Heading",
                "ENGINE_ERROR_Forbidden_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-404", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status404NotFound,
                "ENGINE_ERROR_NotFound_Heading",
                "ENGINE_ERROR_NotFound_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-500", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status500InternalServerError,
                "ENGINE_ERROR_InternalServerError_Heading",
                "ENGINE_ERROR_InternalServerError_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/friendly-502", (HttpContext httpContext) =>
        {
            return RenderFriendlyErrorPageAsync(
                httpContext,
                StatusCodes.Status502BadGateway,
                "ENGINE_ERROR_BadGateway_Heading",
                "ENGINE_ERROR_BadGateway_Message");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    private static IResult RenderFriendlyErrorPageAsync(
        HttpContext httpContext,
        int statusCode,
        string headingKey,
        string messageKey)
    {
        IServiceProvider services = httpContext.RequestServices;
        IStringLocalizer localizer = services.GetRequiredService<IStringLocalizer>();

        string html = new HtmlBuilder().Append($"""
            <!DOCTYPE html>
            <html>
            <head>
                <title>{localizer[headingKey]}</title>
                <meta charset="UTF-8">
            </head>
            <body>
                <h1>{localizer[headingKey]}</h1>
                <p>{localizer[messageKey]}</p>
                <!-- HelloPages -->
            </body>
            </html>
            """).ToString();

        return Results.Content(html, HtmlContentType)
                      .WithStatusCode(statusCode);
    }
}
