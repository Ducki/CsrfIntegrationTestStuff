using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Test;

public static class Extensions
{
    public static void WithoutCsrf(this IWebHostBuilder builder) =>
        builder.ConfigureServices(collection =>
            collection.AddSingleton<IAntiforgery, MyIAntiforgery>());
}

public class MyIAntiforgery : IAntiforgery
{
    public AntiforgeryTokenSet GetAndStoreTokens(HttpContext httpContext)
    {
        return new AntiforgeryTokenSet(
            requestToken: "token",
            cookieToken: "cookietoken",
            formFieldName: "csrf",
            headerName: "AntiForgery");
    }

    public AntiforgeryTokenSet GetTokens(HttpContext httpContext) =>
        new(
            requestToken: "token",
            cookieToken: "cookietoken",
            formFieldName: "csrf",
            headerName: "AntiForgery");

    public async Task<bool> IsRequestValidAsync(HttpContext httpContext) => true;

    public async Task ValidateRequestAsync(HttpContext httpContext)
    {
        // yup, seems legit.
    }

    public void SetCookieTokenAndHeader(HttpContext httpContext)
    {
    }
}