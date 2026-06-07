// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.ExampleNet.Endpoints;
using Xunit;

namespace Genova.ExampleNet.UnitTests.Endpoints;

public class ScannerFiles_Tests
{
    [Theory]
    [InlineData("/path/to/resource.html")]
    [InlineData("/path/to/resource.")]
    [InlineData("/file.txt")]
    [InlineData("file.txt")]
    public void HasFileExtension_should_return_true_for_routes_with_file_extensions(string route)
    {
        // Act
        bool result = ScannerPage.HasFileExtension(route);

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
        bool result = ScannerPage.HasFileExtension(route);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Genova.ExampleNet.wwwroot.index")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.file")]
    [InlineData("Genova.ExampleNet.wwwroot.__.images.keep-it-simple.png")]
    public void GetMarkdownRoute_should_return_null_for_non_markdown_embedded_files(string? embeddedFile)
    {
        // Act
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile!);

        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("Genova.ExampleNet.wwwroot.index.md", "/")]
    [InlineData("Genova.ExampleNet.wwwroot.something.md", "/something")]
    [InlineData("Genova.ExampleNet.wwwroot.something-else.md", "/something-else")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.index.md", "/folder")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.file.md", "/folder/file")]
    public void GetMarkdownRoute_should_return_root_for_index_markdown_embedded_file(string embeddedFile, string expected)
    {
        // Act
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);

        // Assert
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void GetMarkdownRoute_should_return_null_for_null_or_whitespace(string embeddedFile)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("Genova.ExampleNet.wwwroot.index.txt")]
    [InlineData("Genova.ExampleNet.wwwroot.file.html")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.file.png")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.index.JPG")]
    [InlineData("Genova.ExampleNet.wwwroot.index.MD2")]
    public void GetMarkdownRoute_should_return_null_for_non_md_files(string embeddedFile)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("OtherNamespace.wwwroot.index.md")]
    [InlineData("Genova.OtherWeb.wwwroot.index.md")]
    [InlineData("Genova.ExampleNet.wrongroot.index.md")]
    [InlineData("genova.plainoldweb.wwwroot.index.md")] // case-sensitive prefix
    public void GetMarkdownRoute_should_return_null_for_wrong_prefix(string embeddedFile)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("Genova.ExampleNet.wwwroot..md")]
    public void GetMarkdownRoute_should_return_null_for_empty_path(string embeddedFile)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("Genova.ExampleNet.wwwroot.index.md", "/")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.index.md", "/folder")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.subfolder.index.md", "/folder/subfolder")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.file.md", "/folder/file")]
    [InlineData("Genova.ExampleNet.wwwroot.file.md", "/file")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.subfolder.file.md", "/folder/subfolder/file")]
    [InlineData("Genova.ExampleNet.wwwroot.folder.sub.folder.file.md", "/folder/sub/folder/file")]
    public void GetMarkdownRoute_should_return_expected_route_for_valid_markdown_files(string embeddedFile, string expected)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Genova.ExampleNet.wwwroot.folder..index.md", "/folder/")]
    [InlineData("Genova.ExampleNet.wwwroot..index.md", "/")]
    public void GetMarkdownRoute_should_handle_double_dots_and_empty_segments(string embeddedFile, string expected)
    {
        string? result = ScannerPage.GetMarkdownRoute(embeddedFile);
        result.Should().Be(expected);
    }
}
