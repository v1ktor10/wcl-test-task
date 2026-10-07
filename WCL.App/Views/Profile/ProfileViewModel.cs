using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using WCL.App.Services.AvatarLoader;
using WCL.Core.Abstractions;
using WCL.Core.Errors;
using WCL.Core.Models;

namespace WCL.App.Views.Profile;

public sealed partial class ProfileViewModel : SectionViewModel
{
    private readonly IAvatarLoader _avatars;
    private readonly ISessionState _session;

    private CancellationTokenSource? _avatarCts;
    private string? _avatarUrl;

    public ProfileViewModel(ISessionState session, IAvatarLoader avatars)
    {
        _session = session;
        _avatars = avatars;

        session.Changed += (_, _) =>
        {
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(Initials));
            _ = RefreshAvatarAsync();
        };

        _ = RefreshAvatarAsync();
    }

    public override string Title => "Профиль";
    public bool IsAuthenticated => _session.IsAuthenticated;
    public User? User => _session.CurrentUser;

    public string Initials => string.Concat(
        (User?.Name ?? "")
        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Take(2)
        .Select(p => char.ToUpperInvariant(p[0])));

    [ObservableProperty] public partial Bitmap? Avatar { get; private set; }

    private async Task RefreshAvatarAsync()
    {
        string? url = _session.CurrentUser?.AvatarUrl;
        if (url == _avatarUrl) return;
        _avatarUrl = url;

        _avatarCts?.Cancel();
        _avatarCts?.Dispose();
        _avatarCts = null;
        Avatar = null;

        if (string.IsNullOrWhiteSpace(url)) return;

        var cts = _avatarCts = new CancellationTokenSource();
        try
        {
            Avatar = await _avatars.LoadAsync(url, cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
        catch (ServiceException)
        {
            // ignore
        }
    }
}
