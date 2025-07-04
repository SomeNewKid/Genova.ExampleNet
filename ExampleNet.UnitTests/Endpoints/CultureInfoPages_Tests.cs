// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.ExampleNet.Endpoints;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Genova.ExampleNet.UnitTests.Endpoints;

public class CultureInfoPages_Tests
{
    [Fact]
    public void GetRouteValue_should_return_empty_string_when_route_values_are_empty()
    {
        // Arrange
        HttpContext httpContext = new DefaultHttpContext();

        // Act
        string result = CultureInfoPages.GetRouteValue(httpContext, "name");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetRouteValue_should_return_empty_string_when_route_value_does_not_exist()
    {
        // Arrange
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["other"] = "value";

        // Act
        string result = CultureInfoPages.GetRouteValue(httpContext, "name");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetRouteValue_should_return_empty_string_when_route_value_is_null()
    {
        // Arrange
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["name"] = null;

        // Act
        string result = CultureInfoPages.GetRouteValue(httpContext, "name");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetRouteValue_should_return_empty_string_when_route_value_to_string_is_null()
    {
        // Arrange
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["name"] = new ToStringReturnsNull();

        // Act
        string result = CultureInfoPages.GetRouteValue(httpContext, "name");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetRouteValue_should_return_value_when_route_value_exists()
    {
        // Arrange
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["name"] = "en-US";

        // Act
        string result = CultureInfoPages.GetRouteValue(httpContext, "name");

        // Assert
        result.Should().Be("en-US");
    }

    private class ToStringReturnsNull
    {
        public override string? ToString()
        {
            return null;
        }
    }
}
