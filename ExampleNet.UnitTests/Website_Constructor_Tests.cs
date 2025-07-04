// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Configuration;

namespace Genova.ExampleNet.UnitTests;

public class Website_Constructor_Tests
{
    private static IConfiguration BuildConfiguration(string json) =>
        new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            .Build();

    private const string WebsiteId = "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f";
    private const string TenantId = "b9c0d1e2-f3a4-5b6c-7d8e-9f0a1b2c3d4e";

    [Fact]
    public void All_values_present_should_set_all_properties()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "TenantId": "{{TenantId}}",
                        "Hosts": ["host1"],
                        "Localization": {
                            "SupportedCultures": ["en", "fr"],
                            "DefaultCulture": "fr"
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));

        Assert.Equal(Guid.Parse(TenantId), website.TenantId);
        Assert.Equal(["host1"], website.Hosts);
        Assert.Equal(["en", "fr"], website.SupportedCultures);
        Assert.Equal("fr", website.DefaultCulture);
    }

    [Fact]
    public void Missing_tenantSection_should_set_all_defaults()
    {
        string json = "{}";

        Website website = new (BuildConfiguration(json));

        Assert.Empty(website.Hosts);
        Assert.Empty(website.SupportedCultures);
        Assert.Equal("en", website.DefaultCulture);
    }

    [Fact]
    public void Missing_Hosts_should_default_to_empty_array()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Name": "Test"
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Empty(website.Hosts);
    }

    [Fact]
    public void Null_SupportedCultures_should_default_to_empty_array()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "SupportedCultures": null
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Empty(website.SupportedCultures);
    }

    [Fact]
    public void Null_Localization_should_set_DefaultCulture_to_en()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": null
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("en", website.DefaultCulture);
    }

    [Fact]
    public void Missing_DefaultCulture_and_populated_SupportedCultures_should_use_first()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "SupportedCultures": ["es", "de"]
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("es", website.DefaultCulture);
    }

    [Fact]
    public void Missing_DefaultCulture_and_empty_SupportedCultures_should_fallback_to_en()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "SupportedCultures": []
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("en", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_explicitly_null_should_fallback_to_SupportedCultures()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "DefaultCulture": null,
                            "SupportedCultures": ["it"]
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("it", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_null_and_empty_SupportedCultures_should_fallback_to_en()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "DefaultCulture": null,
                            "SupportedCultures": []
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("en", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_set_to_empty_string_should_fallback_to_first_of_SupportedCultures()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "DefaultCulture": "",
                            "SupportedCultures": ["es"]
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("es", website.DefaultCulture);
    }

    [Fact]
    public void SupportedCultures_set_to_invalid_type_should_fallback_to_empty_array()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "SupportedCultures": "notAnArray"
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Empty(website.SupportedCultures);
    }

    [Fact]
    public void DefaultCulture_set_to_whitespace_should_fallback_to_SupportedCultures()
    {
        string json = $$"""
            {
                "Websites": [
                    {
                        "WebsiteId": "{{WebsiteId}}",
                        "Localization": {
                            "DefaultCulture": "   ",
                            "SupportedCultures": [ "jp" ]
                        }
                    }
                ]
            }
            """;

        Website website = new (BuildConfiguration(json));
        Assert.Equal("jp", website.DefaultCulture);
    }

}
