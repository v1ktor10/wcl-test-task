using CommunityToolkit.Mvvm.ComponentModel;
using WCL.App.Services;

namespace WCL.App.Views.Main;

public sealed partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private SectionViewModel _selectedSection;
    private readonly IThemeService _theme;

    public MainViewModel(IEnumerable<SectionViewModel> sections, IThemeService theme)
    {
        Sections = [.. sections];
        _selectedSection = Sections[0];
        _theme = theme;
        theme.Changed += (_, _) => OnPropertyChanged(nameof(IsDarkTheme));
    }

    public bool IsDarkTheme
    {
        get => _theme.IsDark;
        set => _theme.IsDark = value;
    }

    public IReadOnlyList<SectionViewModel> Sections { get; }
}
