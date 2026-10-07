using System.Collections.Concurrent;
using Avalonia.Media.Imaging;
using WCL.Core.Abstractions;
using WCL.Core.Errors;

namespace WCL.App.Services.AvatarLoader;

internal sealed class AvatarLoader(IImageApi images) : IAvatarLoader
{
    private const int DecodeWidth = 128;
    private readonly ConcurrentDictionary<string, Bitmap> _cache = new();

    public async Task<Bitmap> LoadAsync(string url, CancellationToken ct = default)
    {
        if (_cache.TryGetValue(url, out var cached))
            return cached;

        byte[] bytes = await images.DownloadAsync(url, ct);

        Bitmap bitmap;
        try
        {
            using var stream = new MemoryStream(bytes);
            bitmap = Bitmap.DecodeToWidth(stream, DecodeWidth);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or NotSupportedException)
        {
            throw new ServiceException(ErrorKind.Unknown, "Неподдерживаемый формат изображения", ex);
        }

        return _cache.GetOrAdd(url, bitmap);
    }
}
