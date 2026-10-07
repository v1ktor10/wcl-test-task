using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShadUI;
using WCL.App.Views.Auth;

namespace WCL.App.Views.Main;

public sealed partial class MainViewModel : ViewModelBase
{
    private static readonly ThemeVariant[] _variants =
    [
        ThemeVariant.Default,
        ThemeVariant.Light,
        ThemeVariant.Dark
    ];

    private static readonly string[] _icons =
    [
        "◐",
        "☀",
        "☾"
    ];

    private int _themeIndex;

    public MainViewModel(
        IEnumerable<SectionViewModel> sections,
        AuthViewModel auth,
        DialogManager dialogManager,
        ToastManager toastManager)
    {
        Sections = [.. sections];
        SelectedSection = Sections[0];

        Auth = auth;
        DialogManager = dialogManager;
        ToastManager = toastManager;
    }

    [ObservableProperty] public partial SectionViewModel SelectedSection { get; set; }

    [ObservableProperty] public partial string CurrentIcon { get; set; } = _icons[0];

    public IReadOnlyList<SectionViewModel> Sections { get; }

    public AuthViewModel Auth { get; }

    public DialogManager DialogManager { get; }

    public ToastManager ToastManager { get; }

    [RelayCommand]
    private void SwitchTheme()
    {
        _themeIndex = (_themeIndex + 1) % _variants.Length;

        Application.Current!.RequestedThemeVariant =
            _variants[_themeIndex];

        CurrentIcon = _icons[_themeIndex];
    }
}
