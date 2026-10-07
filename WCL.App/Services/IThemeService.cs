namespace WCL.App.Services;

public interface IThemeService
{
    public bool IsDark { get; set; }
    public event EventHandler? Changed;
}
