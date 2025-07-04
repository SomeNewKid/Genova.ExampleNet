// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping culture info-related endpoints.
/// </summary>
internal static class CultureInfoPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the culture info-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the culture info-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET /culture-info
        endpoints.MapGet("/culture-info", (HttpContext httpContext) =>
        {
            // Get all specific culture names from CultureHelper
            IEnumerable<string> specificCultureNames = CultureHelper.GetAllSpecificCultureNames();

            // Generate HTML for the list of cultures
            string html = GenerateCultureListHtml(specificCultureNames);
            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        // GET /culture-info/{name}
        #pragma warning disable ASP0018 // Unused route parameter
        endpoints.MapGet("/culture-info/{name}", (HttpContext httpContext) =>
        {
            string name = GetRouteValue(httpContext, "name");

            try
            {
                // Validate the culture name against the list of supported cultures
                if (!CultureInfo.GetCultures(CultureTypes.AllCultures).Any(
                    c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new CultureNotFoundException($"The culture '{name}' is not supported.");
                }

                // Attempt to create a CultureInfo object from the name
                CultureInfo culture = CultureInfo.GetCultureInfo(name!);

                // Generate HTML for the culture details
                string html = GenerateCultureDetailsHtml(culture);
                return Results.Content(html, HtmlContentType);
            }
            catch
            {
                // Return 404 if the culture is not found
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return Results.Empty;
            }
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
        #pragma warning restore ASP0018 // Unused route parameter
    }

    /// <summary>
    /// Retrieves the value of a route parameter from the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="name">The name of route parameter whose value is to be returned.</param>
    /// <returns>The value of the named route parameter, if found, otherwise an empty string.</returns>
    internal static string GetRouteValue(HttpContext httpContext, string name)
    {
        if (httpContext.Request.RouteValues.ContainsKey(name))
        {
            object? value = httpContext.Request.RouteValues[name];
            if (value != null)
            {
                string? val = value.ToString();
                if (val != null)
                {
                    return val;
                }
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Generates the HTML for the list of specific cultures.
    /// </summary>
    /// <param name="cultureNames">The collection of specific culture names.</param>
    /// <returns>The generated HTML string.</returns>
    private static string GenerateCultureListHtml(IEnumerable<string> cultureNames)
    {
        string listItems = string.Join(Environment.NewLine, cultureNames.Select(cultureName =>
        {
            // Create a CultureInfo object to get the EnglishName
            CultureInfo culture = new(cultureName);
            string cultureSlug = culture.Name.ToLowerInvariant();
            string friendlyName = culture.EnglishName;
            return $"""<li><a href="/culture-info/{cultureSlug}">{friendlyName}</a></li>""";
        }).Where(item => !string.IsNullOrEmpty(item)));

        return new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Culture Info</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Culture Info</h1>
                    <ul>
                        {listItems}
                    </ul>
                </body>
                </html>
                """).ToString();
    }

    /// <summary>
    /// Generates the HTML for the details of a specific culture.
    /// </summary>
    /// <param name="culture">The <see cref="CultureInfo"/> object.</param>
    /// <returns>The generated HTML string.</returns>
    private static string GenerateCultureDetailsHtml(CultureInfo culture)
    {
        return new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Culture Details - {culture.EnglishName}</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Culture Details - {culture.EnglishName}</h1>
                    <p><strong>Name:</strong> {culture.Name}</p>
                    <p><strong>English Name:</strong> {culture.EnglishName}</p>
                    <p><strong>Native Name:</strong> {culture.NativeName}</p>
                    <p><strong>ISO Language Name:</strong> {culture.TwoLetterISOLanguageName}</p>
                    <p><strong>ISO Region Name:</strong> {culture.ThreeLetterISOLanguageName}</p>
                    <p><strong>Region:</strong> {culture.DisplayName}</p>
                </body>
                </html>
                """).ToString();
    }
}
