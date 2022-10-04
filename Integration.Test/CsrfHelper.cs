using System.Net;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Integration.Test;

public class CsrfHelper
{
    public IAntiforgery InnerAntiforgery { get; } = new MyIAntiforgery();
    private readonly AntiforgeryTokenSet _tokenSet;

    public CsrfHelper() =>
        _tokenSet = InnerAntiforgery.GetTokens(new DefaultHttpContext());

    public Cookie GetCsrfCookie() => new(_tokenSet.HeaderName, _tokenSet.CookieToken, "/foo", "localhost");

    public KeyValuePair<string, string> GetCsrfFormValue() => new(_tokenSet.FormFieldName, _tokenSet.RequestToken);
}

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