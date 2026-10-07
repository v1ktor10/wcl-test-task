using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WCL.App.Services.Dialog;
using WCL.App.Services.Notification;
using WCL.Core.Abstractions;
using WCL.Core.Errors;
using WCL.Core.Models.Requests;

namespace WCL.App.Views.Register;

public sealed partial class RegisterViewModel : ViewModelBase
{
    private readonly IAuthService _auth;
    private readonly IDialogService _dialogs;
    private readonly INotificationService _notifications;

    public RegisterViewModel(IAuthService auth,
        IDialogService dialogs,
        INotificationService notifications)
    {
        _auth = auth;
        _dialogs = dialogs;
        _notifications = notifications;

        TrackLoading(SubmitCommand);
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Неверный email")]
    public partial string Email { get; set; } = "";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите имя")]
    [MinLength(2, ErrorMessage = "Минимум 2 символа")]
    public partial string Name { get; set; } = "";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(8, ErrorMessage = "Минимум 8 символов")]
    public partial string Password { get; set; } = "";

    [RelayCommand]
    private async Task SubmitAsync()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        try
        {
            await _auth.RegisterAsync(new RegisterRequest(Email.Trim(), Name.Trim(), Password));
            _dialogs.Close(this);
            _notifications.ShowSuccess("Регистрация завершена", "Вы вошли в систему");
        }
        catch (ServiceException ex) when (ex.Kind == ErrorKind.AutoLoginFailed)
        {
            _dialogs.Close(this);
            _notifications.ShowError("Аккаунт создан", "Войдите вручную");
        }
        catch (ServiceException ex)
        {
            _notifications.ShowError("Не удалось зарегистрироваться", ex.ToUserMessage());
        }
    }

    [RelayCommand]
    private void Cancel() => _dialogs.Close(this);
}
