// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Execution;
using Genova.Common.Headers;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Http;

namespace Genova.ExampleNet.Headers;

/// <summary>
/// Adds the X-Website-Name header if it is missing.
/// </summary>
internal sealed class XWebsiteNameModifier : IHeadersModifier
{
    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
    }

    /// <inheritdoc/>
    public void Modify(
        IHeaderDictionary responseHeaders, string? responseContentType, string requestHost, string requestPath)
    {
        // Add the X-Website-Name header if it is not already present.
        if (!responseHeaders.ContainsKey("X-Website-Name"))
        {
            responseHeaders["X-Website-Name"] = Website.NamePrefix;
        }
    }
}
