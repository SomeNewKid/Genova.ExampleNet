// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Utilities;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.ExampleNet.Endpoints;

/// <summary>
/// Provides methods for mapping file-related endpoints.
/// </summary>
internal static class WwwFiles
{
    /// <summary>
    /// Maps the file-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the file-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoints for supported file extensions
        MapFileEndpoint(endpoints, "ico");
        MapFileEndpoint(endpoints, "png");
    }

    /// <summary>
    /// Normalizes a filename in a URL to the name of an embedded resource.
    /// </summary>
    /// <param name="filename">The filename in a URL.</param>
    /// <returns>The name of the embedded resource.</returns>
    internal static string? NormalizeFilename(string? filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return filename;
        }

        return filename.Replace('.', '~');
    }

    /// <summary>
    /// Normalizes a path in a URL to the path of an embedded resource.
    /// </summary>
    /// <param name="path">The path in a URL.</param>
    /// <returns>The path of the embedded resource.</returns>
    internal static string NormalizeFilePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        return path.Replace("/-/", "/__/");
    }

    /// <summary>
    /// Maps a file-related endpoint for a specific file extension and path template.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the endpoint.</param>
    /// <param name="extension">The file extension to map (e.g., "ico", "png").</param>
    private static void MapFileEndpoint(IEndpointRouteBuilder endpoints, string extension)
    {
        endpoints.MapGet($"/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = httpContext.Request.RouteValues["filename"] as string;
            string pathTemplate = "wwwroot/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/images/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = httpContext.Request.RouteValues["filename"] as string;
            string pathTemplate = "wwwroot/-/images/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Serves an embedded file based on the provided path template and extension.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="pathTemplate">The template for constructing the embedded file path.</param>
    /// <param name="filename">The file name of the requested file.</param>
    /// <param name="extension">The file extension of the requested file.</param>
    private static IResult ServeFileFromTemplate(
        HttpContext context, string pathTemplate, string? filename, string extension)
    {
        string normalizedPath = NormalizeFilePath(pathTemplate);
        string? normalizedFilename = NormalizeFilename(filename);

        if (!string.IsNullOrWhiteSpace(normalizedFilename))
        {
            // Construct the embedded file path using the template
            string embeddedFilePath = string.Format(normalizedPath, normalizedFilename, extension);

            // Serve the embedded file
            FileHelper.ServeEmbeddedFileAsync(context, typeof(Website), embeddedFilePath).GetAwaiter().GetResult();
            return Results.Empty;
        }
        else
        {
            // Return 404 if the filename is missing
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Results.Empty;
        }
    }
}
