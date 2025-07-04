// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Html;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping the /hello/headers endpoint.
/// </summary>
internal static class HelloHeadersPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the /hello/headers endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the /hello/headers endpoint.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello/headers", (HttpContext httpContext) =>
        {
            // Create a dictionary of headers to inject into the response
            Dictionary<string, string> responseHeaders = new()
            {
                { "X-Powered-By", "PHP/7.4.3" },
                { "Server", "nginx/1.18.0" },
            };

            // Add each header to the HttpResponse
            foreach (KeyValuePair<string, string> header in responseHeaders)
            {
                httpContext.Response.Headers[header.Key] = header.Value;
            }

            // Build the HTML content to display the injected headers
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>HTTP Headers</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Injected HTTP Headers</h1>
                    <table border="1">
                        <thead>
                            <tr>
                                <th>Header</th>
                                <th>Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            {string.Join("\n", responseHeaders.Select(
                                        header => $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>")):raw}
                        </tbody>
                    </table>
                    <!-- HelloHeadersPages -->
                    <!-- ExampleNet -->
                </body>
                </html>
                """).ToString();

            // Return the HTML content as a Results response
            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
