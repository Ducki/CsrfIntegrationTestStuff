using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Integration.Test;

public class IntegrationTest
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly CsrfHelper _csrfHelper;

    public IntegrationTest()
    {
        _csrfHelper = new CsrfHelper();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.WithCsrf(_csrfHelper));
    }

    [Fact]
    public async Task Test1()
    {
        var c = _factory.CreateCsrfClient(_csrfHelper.GetCsrfCookie());

        var formFields = new List<KeyValuePair<string, string>>
        {
            new("Foo", "Bar"),
            _csrfHelper.GetCsrfFormValue()
        };

        var content = new FormUrlEncodedContent(formFields);
        var result = await c.PostAsync("/Index", content);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}