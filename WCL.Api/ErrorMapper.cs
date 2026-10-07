using System.Net;
using System.Text.Json;
using WCL.Core.Errors;

namespace WCL.Api;

internal static class ErrorMapper
{
    public static async Task<ServiceException> ToExceptionAsync(
        HttpResponseMessage response, CancellationToken ct)
    {
        string message = await TryReadMessageAsync(response, ct) ?? "Failed to complete request";
        return new ServiceException(ToKind(response.StatusCode), message);
    }

    private static ErrorKind ToKind(HttpStatusCode status) => (int)status switch
    {
        401 or 403 => ErrorKind.Unauthorized,
        404 => ErrorKind.NotFound,
        400 or 409 or 422 => ErrorKind.Validation,
        >= 500 => ErrorKind.Server,
        _ => ErrorKind.Unknown
    };

    private static async Task<string?> TryReadMessageAsync(
        HttpResponseMessage response, CancellationToken ct)
    {
        try
        {
            await using var stream = await response.Content.ReadAsStreamAsync(ct);
            using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return null;

            foreach (string name in new[] { "message", "error" })
                if (root.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String)
                    return value.GetString();
        }
        catch (JsonException)
        {
            // ignore
        }

        return null;
    }
}
