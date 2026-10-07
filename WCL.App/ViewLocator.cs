using Avalonia.Controls;
using Avalonia.Controls.Templates;
using WCL.App.Views;
using WCL.App.Views.About;
using WCL.App.Views.Auth;
using WCL.App.Views.Profile;

namespace WCL.App;

public sealed class ViewLocator : IDataTemplate
{
    public Control? Build(object? data) => data switch
    {
        ProfileViewModel => new ProfileView(),
        AboutViewModel => new AboutView(),
        AuthViewModel => new AuthView(),
        null => null,
        _ => new TextBlock { Text = $"View for {data.GetType().Name} not found" }
    };

    public bool Match(object? data) => data is ViewModelBase;
}
