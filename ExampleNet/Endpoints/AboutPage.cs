// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Net;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Razor;
using Genova.Common.Websites;
using Genova.Content;
using Genova.Content.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping redirected endpoints.
/// </summary>
internal static class AboutPage
{
    /// <summary>
    /// Maps the /about endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the /about endpoint.
    /// </param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // GET about
        endpoints.MapGet("/about", async (HttpContext httpContext) =>
        {
            IExecutionContextAccessor executionContextAccessor =
                httpContext.RequestServices.GetRequiredService<IExecutionContextAccessor>();
            IExecutionContext executionContext = executionContextAccessor.Current;
            IContentModule contentModule =
                httpContext.RequestServices.GetRequiredKeyedService<IContentModule>(Website.Identifier);
            string requestCulture = executionContext.CultureContext.Request;
            string defaultCulture = executionContext.CultureContext.Default;
            Article? article = await contentModule.GetArticleByKeyCultureAsync(
                                     "ARTICLE_About_Page", requestCulture, defaultCulture);
            Snippet? snippet = await contentModule.GetSnippetByKeyCultureAsync(
                                     "SNIPPET_Footer_Links", requestCulture, defaultCulture);

            string title = "About Page";

            HtmlBuilder htmlBuilder = new ();

            htmlBuilder.Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>{title}</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>{title}</h1>
                    <div>
                        {article?.Value}
                    </div>
                    <footer>
                        <p>{snippet?.Value}</p>
                    </footer>
                </body>
                </html>
                """);

            return Results.Content(htmlBuilder.ToString(), "text/html; charset=utf-8");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
