// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Crawler;
using Genova.Scanner;
using Genova.Testing.ScannerTests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genova.ExampleNet.ScannerTests;

[TestClass]
public class RuleScanner : RuleScanner_Base<Host.Program>
{
    readonly Website _website;

    public RuleScanner()
    {
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(AppSettings));
        IConfiguration configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
        _website = new(configuration);
    }

    protected override string AppSettings
    {
        get
        {
            return """
            {
              "Logging": {
                "LogLevel": {
                  "Default": "Information",
                  "Microsoft.AspNetCore": "Warning",
                  "Microsoft.AspNetCore.Mvc.Razor": "Debug",
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
        }
    }

    protected override string Host
    {
        get
        {
            return "https://www.example.net";
        }
    }

    protected override CrawlOptions CrawlOptions
    {
        get
        {
            return new CrawlOptions
            {
                PauseBetweenRequests = 100,
                StartingPaths = ["/sitemap.xml", "/scanner", "/login", "/hello/form"],
            };
        }
    }

    protected override ScanOptions ScanOptions
    {
        get
        {
            return new()
            {
                CorsPolicy = _website.GetCorsPolicy(),
                Thoroughness = 0.5m,
            };
        }
    }
}
