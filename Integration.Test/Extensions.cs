using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Test;

public static class Extensions
{
    public static void WithCsrf(this IWebHostBuilder builder, CsrfHelper csrfHelper) =>
        builder.ConfigureServices(collection =>
            collection.AddSingleton(_ => csrfHelper.InnerAntiforgery));

    public static HttpClient CreateCsrfClient<TEntryPoint>(
        this WebApplicationFactory<TEntryPoint> factory,
        Cookie csrfCookie) where TEntryPoint : class
    {
        var cookieHandler = new CookieContainerHandler();
        cookieHandler.Container.Add(csrfCookie);
        return factory.CreateDefaultClient(cookieHandler);
    }
}