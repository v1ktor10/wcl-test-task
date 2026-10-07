using WCL.Core.Abstractions;
using WCL.Core.Errors;

namespace WCL.Api;

internal sealed class ImageApi(HttpClient http) : IImageApi
{
    public async Task<byte[]> DownloadAsync(string url, CancellationToken ct = default)
    {
        try
        {
            return await http.GetByteArrayAsync(url, ct);
        }
        catch (HttpRequestException ex)
        {
            throw new ServiceException(ErrorKind.Network, "Не удалось загрузить изображение", ex);
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            throw new ServiceException(ErrorKind.Network, "Timeout", ex);
        }
    }
}
