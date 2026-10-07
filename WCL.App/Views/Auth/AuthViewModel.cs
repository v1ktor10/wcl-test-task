using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WCL.App.Services.Dialog;
using WCL.App.Services.Notification;
using WCL.App.Views.Register;
using WCL.Core.Abstractions;
using WCL.Core.Errors;
using WCL.Core.Models.Requests;

namespace WCL.App.Views.Auth;

public sealed partial class AuthViewModel : ViewModelBase
{
    private readonly IAuthService _auth;
    private readonly IDialogService _dialogs;
    private readonly INotificationService _notifications;
    private readonly ISessionState _session;

    public AuthViewModel(
        IAuthService auth,
        ISessionState session,
        INotificationService notifications,
        IDialogService dialogs)
    {
        _auth = auth;
        _session = session;
        _notifications = notifications;
        _dialogs = dialogs;

        Email = "";
        Password = "";

        session.Changed += (_, _) =>
        {
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(UserName));
        };

        TrackLoading(LoginCommand, LogoutCommand);
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    public partial string Email { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    public partial string Password { get; set; }

    public bool IsAuthenticated => _session.IsAuthenticated;
    public string? UserName => _session.CurrentUser?.Name;

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        try
        {
            await _auth.LoginAsync(new LoginRequest(Email.Trim(), Password));

            Password = "";
            _notifications.ShowSuccess("Вы вошли", UserName);
        }
        catch (ServiceException ex)
        {
            _notifications.ShowError("Не удалось войти", ex.ToUserMessage());
        }
    }

    private bool CanLogin() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrEmpty(Password);

    [RelayCommand]
    private void Register() => _dialogs.Show<RegisterViewModel>();

    [RelayCommand]
    private async Task LogoutAsync() => await _auth.LogoutAsync();
}
