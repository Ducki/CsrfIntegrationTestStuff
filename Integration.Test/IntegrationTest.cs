using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Integration.Test;

public class IntegrationTest
{
    private readonly WebApplicationFactory<Program> _factory =
        new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.WithoutCsrf());


    [Fact]
    public async Task ShouldPostWithSuccessWithoutCsrfToken()
    {
        // Arrange
        var client = _factory.CreateClient();

        var formFields = new List<KeyValuePair<string, string>>
        {
            new("Foo", "Bar")
        };

        var content = new FormUrlEncodedContent(formFields);

        // Act
        var result = await client.PostAsync("/Index", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}