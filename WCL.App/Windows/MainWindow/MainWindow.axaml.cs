using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace WCL.App.Windows.MainWindow;

public partial class MainWindow : ShadUI.Window
{
    private static readonly ThemeVariant[] _variants =
        [ThemeVariant.Default, ThemeVariant.Light, ThemeVariant.Dark];

    private static readonly string[] _icons = ["◐", "☀", "☾"];

    private int _index;

    public MainWindow() => InitializeComponent();

    private void OnSwitchThemeClick(object? sender, RoutedEventArgs e)
    {
        _index = (_index + 1) % _variants.Length;
        Application.Current!.RequestedThemeVariant = _variants[_index];
        
        if (sender is Button { Content: TextBlock icon })
            icon.Text = _icons[_index];
    }
}
