// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Html;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping sitemap endpoints.
/// </summary>
/// <remarks>
/// For more information, see https://www.sitemaps.org/protocol.html.
/// </remarks>
internal static class SitemapPage
{
    /// <summary>
    /// Maps the sitemap endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the sitemap endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Existing /sitemap.xml endpoint
        endpoints.MapGet("/sitemap.xml", (HttpContext httpContext) =>
        {
            string domain = httpContext.Request.Host.ToString();
            IEnumerable<string?> routes = GetRoutes(httpContext);

            // Build the XML sitemap content
            string set = string.Join("\n", routes.Select(route => $"""
                    <url>
                        <loc>https://{domain}{route}</loc>
                    </url>
                """));

            string xml = $"""
                <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
                {set}
                </urlset>
                """;

            return Results.Text(xml, "application/xml");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        // New /sitemap endpoint
        endpoints.MapGet("/sitemap", (HttpContext httpContext) =>
        {
            IEnumerable<string?> routes = GetRoutes(httpContext);

            // Build the HTML sitemap content
            string listItems = string.Join("\n", routes.Select(route => $"""
                <li><a href="{route}">{route}</a></li>
            """));

            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Sitemap</title>
                </head>
                <body>
                    <h1>Sitemap</h1>
                    <ul>
                        {listItems:raw}
                    </ul>
                </body>
                </html>
                """).ToString();

            return Results.Text(html, "text/html");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Checks if the given route is to a file with an extension.
    /// </summary>
    /// <param name="route">The route.</param>
    /// <returns><c>true</c> if the route is to a file with an extension, otherwise <c>false</c>.</returns>
    internal static bool HasFileExtension(string route)
    {
        string[] slugs = route.Split('?')[0].Split('/');
        string finalSlug = slugs[slugs.Length - 1];
        return finalSlug.Contains('.');
    }

    /// <summary>
    /// Retrieves all valid routes for the sitemap.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>A collection of valid routes.</returns>
    private static HashSet<string?> GetRoutes(HttpContext httpContext)
    {
        // Get the EndpointDataSource service
        EndpointDataSource endpointDataSource =
            httpContext.RequestServices.GetRequiredService<EndpointDataSource>();

        // Retrieve all registered endpoints and exclude those with pattern matching or file extensions
        IEnumerable<string?> routes = endpointDataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Where(endpoint => IsGetMethod(endpoint))
            .Where(endpoint => endpoint.Metadata.GetMetadata<WebsiteRouteInfo>()?.WebsiteId == Website.Identifier)
            .Select(endpoint => endpoint.RoutePattern.RawText)
            .Where(route => route != null
                            && !route.Contains('{')
                            && !route.Contains('}')
                            && !HasFileExtension(route))
            .Distinct();

        // Modify the routes collection
        return ModifyRoutes(routes);
    }

    /// <summary>
    /// Modifies the routes collection by removing and adding specific routes.
    /// </summary>
    /// <param name="routes">The original collection of routes.</param>
    /// <returns>The modified collection of routes.</returns>
    private static HashSet<string?> ModifyRoutes(IEnumerable<string?> routes)
    {
        // Convert to a HashSet for efficient modification
        HashSet<string?> routeSet = [.. routes];

        // Login page
        routeSet.Add("/login");

        return routeSet;
    }

    private static bool IsGetMethod(RouteEndpoint endpoint)
    {
        HttpMethodMetadata? httpMethodMetadata = endpoint.Metadata.GetMetadata<HttpMethodMetadata>();
        return httpMethodMetadata?.HttpMethods.Contains("GET") ?? false;
    }
}
