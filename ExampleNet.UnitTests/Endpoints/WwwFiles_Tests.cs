// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.ExampleNet.Endpoints;
using Xunit;

namespace Genova.ExampleNet.UnitTests.Endpoints;

public class WwwFiles_Tests
{
    [Fact]
    public void NormalizeFilename_should_return_null_when_filename_is_null()
    {
        // Act
        string? result = WwwFiles.NormalizeFilename(null!);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void NormalizeFilename_should_return_empty_when_filename_is_empty()
    {
        // Act
        string? result = WwwFiles.NormalizeFilename(string.Empty);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void NormalizeFilename_should_return_whitespace_when_filename_is_whitespace()
    {
        // Arrange
        string filename = "   ";

        // Act
        string? result = WwwFiles.NormalizeFilename(filename);

        // Assert
        result.Should().Be(filename);
    }

    [Fact]
    public void NormalizeFilename_should_return_same_filename_when_no_dots_are_present()
    {
        // Arrange
        string filename = "kitten";

        // Act
        string? result = WwwFiles.NormalizeFilename(filename);

        // Assert
        result.Should().Be(filename);
    }

    [Fact]
    public void NormalizeFilename_should_replace_dots_with_tildes_when_dots_are_present()
    {
        // Arrange
        string filename = "kitten.en";

        // Act
        string? result = WwwFiles.NormalizeFilename(filename);

        // Assert
        result.Should().Be("kitten~en");
    }

    [Fact]
    public void NormalizeFilePath_should_return_empty_when_path_is_empty()
    {
        // Act
        string? result = WwwFiles.NormalizeFilePath(string.Empty);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void NormalizeFilePath_should_return_whitespace_when_path_is_whitespace()
    {
        // Arrange
        string path = "   ";

        // Act
        string? result = WwwFiles.NormalizeFilePath(path);

        // Assert
        result.Should().Be(path);
    }

    [Fact]
    public void NormalizeFilePath_should_return_same_path_when_no_dash_pattern_is_present()
    {
        // Arrange
        string path = "/images/";

        // Act
        string? result = WwwFiles.NormalizeFilePath(path);

        // Assert
        result.Should().Be(path);
    }

    [Fact]
    public void NormalizeFilePath_should_replace_dash_pattern_with_double_underscore()
    {
        // Arrange
        string path = "/-/images/";

        // Act
        string? result = WwwFiles.NormalizeFilePath(path);

        // Assert
        result.Should().Be("/__/images/");
    }
}
