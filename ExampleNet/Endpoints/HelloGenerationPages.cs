// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using Genova.Common.Websites;
using Genova.Generation.Gateways;
using Genova.Generation.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping the /hello/generation endpoint.
/// </summary>
internal static class HelloGenerationPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the /hello/generation endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">
    /// The <see cref="IEndpointRouteBuilder"/> to which to add the /hello/generation endpoint.
    /// </param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello/generation", async (HttpContext httpContext) =>
        {
            IOpenAiApiGateway openAiApiGateway =
                httpContext.RequestServices.GetRequiredKeyedService<IOpenAiApiGateway>(Website.Identifier);

            string sentence = await GetGeneratedSentence(openAiApiGateway);

            string html = $"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                  <meta charset="utf-8">
                  <title>Generation Page</title>
                  <meta name="viewport" content="width=device-width,initial-scale=1">
                </head>
                <body>

                    <h1>Text generation by OpenAI API</h1>
                    <p>{sentence}</p>

                </body>
                </html>
                """;

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    [ExcludeFromCodeCoverage(Justification = "This method handles errors from third-party API.")]
    private static async Task<string> GetGeneratedSentence(IOpenAiApiGateway openAiApiGateway)
    {
        OpenAiTextRequest textRequest = new()
        {
            Model = "gpt-4",
            Context = "You are a helpful assistant.",
            Prompt = "Please generate a sentence which includes the word `puppy` (in lower-case letters).",
        };

        OpenAiTextResponse textResponse = await openAiApiGateway.GetTextResponseAsync(textRequest);

        string sentence = textResponse.Success ? textResponse.Content : "";

        if (string.IsNullOrWhiteSpace(sentence))
        {
            sentence = "Generation failed for a sentence including the word `puppy`.";
        }

        return sentence;
    }
}
