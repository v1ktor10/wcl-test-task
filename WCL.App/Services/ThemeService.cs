using Avalonia;
using Avalonia.Styling;

namespace WCL.App.Services;

internal sealed class ThemeService : IThemeService
{
    public ThemeService()
    {
        if (Application.Current is { } app)
            app.ActualThemeVariantChanged += (_, _) => Changed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? Changed;

    public bool IsDark
    {
        get => Application.Current?.ActualThemeVariant == ThemeVariant.Dark;
        set
        {
            if (Application.Current is { } app)
                app.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
        }
    }
}
