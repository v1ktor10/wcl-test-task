namespace WCL.Core.Abstractions;

public interface IImageApi
{
    public Task<byte[]> DownloadAsync(string url, CancellationToken ct = default);
}
