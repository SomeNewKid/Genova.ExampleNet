// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.ExampleNet.Endpoints;
using Xunit;

namespace Genova.ExampleNet.UnitTests.Endpoints;

public class SitemapFiles_Tests
{
    [Theory]
    [InlineData("/path/to/resource.html")]
    [InlineData("/path/to/resource.")]
    [InlineData("/file.txt")]
    [InlineData("file.txt")]
    public void HasFileExtension_should_return_true_for_routes_with_file_extensions(string route)
    {
        // Act
        bool result = SitemapPage.HasFileExtension(route);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("route_without_extension")]
    [InlineData("/path/to/resource")]
    [InlineData("file")]
    [InlineData("route_without_extension?query=file.txt")]
    [InlineData("/path/to/resource?query=file.txt")]
    [InlineData("file?query=file.txt")]
    public void HasFileExtension_should_return_false_for_routes_without_file_extensions(string route)
    {
        // Act
        bool result = SitemapPage.HasFileExtension(route);

        // Assert
        result.Should().BeFalse();
    }
}
