// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.Common.Execution;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Genova.ExampleNet.UnitTests;

public class Website_Tests
{
    [Fact]
    public void Website_name()
    {
        // Arrange
        string appsettings = """
            {
                "Websites": [
                    {
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "Name": "Example"
                    }
                ]
            }
            """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        // Act
        Website website = new (configuration);

        // Assert
        Assert.Equal("Example", website.Name);
    }

    [Fact]
    public void Website_Hosts_should_default_to_empty_array_when_Hosts_is_not_set()
    {
        // Arrange
        string appsettings = """
            {
                "Websites": [
                    {
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "Name": "Example"
                    }
                ]
            }
            """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        // Act
        Website website = new (configuration);

        // Assert
        Assert.Empty(website.Hosts);
    }

    [Fact]
    public void Website_Hosts_should_be_empty_when_configuration_is_missing_Tenants_section()
    {
        // Arrange
        string appsettings = "{}";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        // Act
        Website website = new (configuration);

        // Assert
        Assert.Empty(website.Hosts);
    }

    [Fact]
    public void DefaultCulture_should_be_set_from_Localization_DefaultCulture()
    {
        // Arrange
        string appsettings = """
            {
                "Websites": [
                    {
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "Name": "Example",
                        "Hosts": [
                            "localhost:7282",
                            "www.example.net"
                        ],
                        "Localization": {
                            "DefaultCulture": "fr"
                        }
                    }
                ]
            }
            """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        Website website = new (configuration);

        // Act & Assert
        Assert.Equal("fr", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_should_be_set_from_SupportedCultures_if_Localization_DefaultCulture_is_not_set()
    {
        // Arrange
        string appsettings = """
            {
                "Websites": [
                    {
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "Name": "Example",
                        "Hosts": [
                            "localhost:7282",
                            "www.example.net"
                        ],
                        "Localization": {
                            "SupportedCultures": [ "es", "de" ]
                        }
                    }
                ]
            }
            """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        Website website = new (configuration);

        // Act & Assert
        Assert.Equal("es", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_should_default_to_en_if_neither_Localization_DefaultCulture_nor_SupportedCultures_are_set()
    {
        // Arrange
        string appsettings = """
            {
                "Websites": [
                    {
                        "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                        "Name": "Example",
                        "Hosts": [
                            "localhost:7282",
                            "www.example.net"
                        ]
                    }
                ]
            }
            """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        Website website = new (configuration);

        // Act & Assert
        Assert.Equal("en", website.DefaultCulture);
    }

    [Fact]
    public void DefaultCulture_should_fallback_to_en_when_SupportedCultures_is_empty()
    {
        // Arrange
        string appsettings = """
        {
            "Websites": [
                {
                    "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                    "Localization": {
                        "DefaultCulture": null,
                        "SupportedCultures": []
                    }
                }
            ]
        }
        """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        // Act
        Website website = new (configuration);

        // Assert
        Assert.Equal("en", website.DefaultCulture);
    }


    [Fact]
    public void SupportedCultures_should_fallback_to_empty_array_when_configured_as_null()
    {
        // Arrange
        string appsettings = """
        {
            "Websites": [
                {
                    "WebsiteId": "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f",
                    "Localization": {
                        "SupportedCultures": null
                    }
                }
            ]
        }
        """;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(appsettings)))
            .Build();

        // Act
        Website website = new (configuration);

        // Assert
        Assert.Empty(website.SupportedCultures);
    }

    [Fact]
    public void GetRazorViewName_should_return_null_for_unknown_model_type()
    {
        // Arrange
        IExecutionContext executionContext = new Mock<IExecutionContext>().Object;
        IConfiguration configuration = new ConfigurationBuilder().Build();
        Website website = new (configuration);
        object unknownModel = new { SomeProperty = "SomeValue" };

        // Act
        string? viewName = website.GetRazorViewName(executionContext, unknownModel);

        // Assert
        viewName.Should().BeNull();
    }
}
