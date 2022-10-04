using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Integration.Test;

public class MyIAntiforgery : IAntiforgery
{
    public AntiforgeryTokenSet GetAndStoreTokens(HttpContext httpContext)
    {
        return new AntiforgeryTokenSet("token", "cookietoken", "csrf", headerName: null);
    }

    public AntiforgeryTokenSet GetTokens(HttpContext httpContext) =>
        new("token", "cookietoken", "csrf", headerName: "AntiForgery");

    public async Task<bool> IsRequestValidAsync(HttpContext httpContext) => throw new NotImplementedException();

    public async Task ValidateRequestAsync(HttpContext httpContext)
    {
        // yup, seems legit.
    }

    public void SetCookieTokenAndHeader(HttpContext httpContext) => throw new NotImplementedException();
}