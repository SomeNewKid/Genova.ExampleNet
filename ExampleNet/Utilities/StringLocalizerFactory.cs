// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Genova.ExampleNet.Utilities;

/// <summary>
/// Factory for creating a <see cref="FallbackStringLocalizer"/>.
/// </summary>
internal sealed class StringLocalizerFactory
{
    private readonly string _defaultCulture;
    private readonly string _resourcesPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringLocalizerFactory"/> class.
    /// </summary>
    /// <param name="defaultCulture">The default culture for the website.</param>
    /// <param name="resourcesPath">The path to the resources folder.</param>
    public StringLocalizerFactory(string defaultCulture, string resourcesPath = "Resources")
    {
        _defaultCulture = defaultCulture;
        _resourcesPath = resourcesPath;
    }

    /// <summary>
    /// Creates an <see cref="IStringLocalizer"/> for the website.
    /// </summary>
    /// <returns>An <see cref="IStringLocalizer"/> instance.</returns>
    public IStringLocalizer CreateLocalizer()
    {
        // Manually instantiate a localization factory
        LocalizationOptions locOptions = new()
        {
            ResourcesPath = _resourcesPath,
        };
        ResourceManagerStringLocalizerFactory factory = new(
            Options.Create(locOptions),
            NullLoggerFactory.Instance);

        // Create a localizer for the marker class "SharedResource" in the same assembly
        IStringLocalizer inner = factory.Create(typeof(SharedResource));

        // Wrap it in a FallbackStringLocalizer
        return new FallbackStringLocalizer(inner, _defaultCulture);
    }
}
