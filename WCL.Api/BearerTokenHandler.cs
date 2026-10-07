using System.Net.Http.Headers;
using WCL.Core.Abstractions;

namespace WCL.Api;

internal sealed class BearerTokenHandler(ISessionState session) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        if (session.Tokens is { } tokens)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        return base.SendAsync(request, ct);
    }
}
