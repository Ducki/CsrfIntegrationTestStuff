using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Test;

public static class Extensions
{
    public static void WithoutCsrf(this IWebHostBuilder builder) =>
        builder.ConfigureServices(collection =>
            collection.AddSingleton<IAntiforgery, MyIAntiforgery>());
}