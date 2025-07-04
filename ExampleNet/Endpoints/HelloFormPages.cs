// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Web;
using Genova.Common.Filters;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping form-related endpoints.
/// </summary>
internal static class HelloFormPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the form-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the form-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET /hello/form
        endpoints.MapGet("/hello/form", async (HttpContext httpContext) =>
        {
            CsrfHelper csrfHelper = new(httpContext);
            AntiforgeryTokenSet tokens = await csrfHelper.Tokenize();

            string html = GenerateFormHtml(
                              "Hello, Form",
                              "Enter your message below:",
                              tokens.RequestToken!);

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        // POST /hello/form
        endpoints.MapPost("/hello/form", async (HttpContext httpContext) =>
        {
            CsrfHelper csrfHelper = new(httpContext);
            await csrfHelper.Validate();
            AntiforgeryTokenSet tokens = await csrfHelper.Tokenize();

            string? formData = httpContext.Request.Form["message"];
            string encodedMessage = HttpUtility.HtmlEncode(formData ?? string.Empty);

            string html = GenerateFormHtml(
                              "Form Submitted",
                              $"You entered: {encodedMessage}",
                              tokens.RequestToken!);

            return Results.Content(html, HtmlContentType);
        })
        .AddEndpointFilter<CrossSiteScriptingFilter>()
        .AddEndpointFilter<OsCommandInjectionFilter>()
        .AddEndpointFilter<SqlInjectionFilter>()
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Generates the HTML for the form page.
    /// </summary>
    /// <param name="title">The title of the HTML document.</param>
    /// <param name="message">The message to display above the form.</param>
    /// <param name="csrfToken">The anti-forgery token to include in the form.</param>
    /// <returns>The generated HTML string.</returns>
    private static string GenerateFormHtml(string title, string message, string csrfToken)
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
                    <form method="post" action="/hello/form">
                        <input type="hidden" name="{CsrfHelper.FieldName}" value="{csrfToken}" />
                        <label for="message">Message:</label>
                        <input type="text" id="message" name="message" />
                        <button type="submit">Submit</button>
                    </form>
                    <!-- HelloPages -->
                </body>
                </html>
                """).ToString();
    }
}
