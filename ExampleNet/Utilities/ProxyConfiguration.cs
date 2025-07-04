// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Yarp.ReverseProxy.Configuration;

namespace Genova.ExampleNet.Utilities;

/// <summary>
/// Represents the configuration for the website proxy.
/// </summary>
internal sealed class ProxyConfiguration
{
    private readonly string _name;
    private readonly string[] _hosts;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProxyConfiguration"/> class.
    /// </summary>
    /// <param name="name">The name of the website.</param>
    /// <param name="hosts">The hosts for the website.</param>
    internal ProxyConfiguration(string name, string[] hosts)
    {
        _name = name;
        _hosts = hosts;
    }

    /// <summary>
    /// Gets the route configurations for the website proxy.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="RouteConfig"/> objects.</returns>
    public IEnumerable<RouteConfig> GetRoutes()
    {
        return
        [
            new RouteConfig
            {
                RouteId = _name + "Route",
                ClusterId = _name + "ExampleCluster",
                Match = new RouteMatch
                {
                    Hosts = _hosts,
                    Path = "/example/{**catch-all}",
                },
                Transforms =
                [
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/example" },
                    },
                ],
            }

        ];
    }

    /// <summary>
    /// Gets the cluster configurations for the website proxy.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="ClusterConfig"/> objects.</returns>
    public IEnumerable<ClusterConfig> GetClusters()
    {
        return
        [
            new ClusterConfig
            {
                ClusterId = _name + "ExampleCluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        "exampleBackend", new DestinationConfig
                        {
                            Address = "https://www.example.net",
                        }
                    },
                },
            },
        ];
    }
}
