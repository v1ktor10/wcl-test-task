using Avalonia.Media.Imaging;

namespace WCL.App.Services.AvatarLoader;

public interface IAvatarLoader
{
    public Task<Bitmap> LoadAsync(string url, CancellationToken ct = default);
}
