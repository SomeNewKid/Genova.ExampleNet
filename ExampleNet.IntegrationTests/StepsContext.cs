// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Reqnroll;

namespace Genova.ExampleNet.IntegrationTests;

[Binding]
public class StepsContext : Testing.IntegrationTests.StepsContext<Host.Program>
{
    private const string AppSettings = """
            {
                "Logging": {
                    "LogLevel": {
                        "Default": "Information",
                        "Microsoft.AspNetCore": "Warning",
                        "Microsoft.AspNetCore.Mvc.Razor": "Debug",
                        "Microsoft.ReverseProxy.Matching": "Debug",
                        "Microsoft.Hosting.Lifetime": "Information"
                    }
                },
                "AllowedHosts": "*",
                "Websites": [
                    {
                        "Name": "example-net",
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "TenantId": "b9c0d1e2-f3a4-5b6c-7d8e-9f0a1b2c3d4e",
                        "Hosts": [
                            "localhost:7282",
                            "www.example.net"
                        ],
                        "Settings": {
                            "Setting1": "Value2.1",
                            "Setting2": "Value2.2"
                        },
                        "Localization": {
                            "DefaultCulture": "en"
                        }
                    }
                ]
            }        
            """;

    public StepsContext() : base(AppSettings)
    {
    }
}
