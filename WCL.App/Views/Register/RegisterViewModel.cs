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
        catch (ServiceException ex)
        {
            if (ex.Kind == ErrorKind.Unauthorized)
            {
                _dialogs.Close(this);
                _notifications.ShowError("Аккаунт создан", "Войдите вручную");
                return;
            }

            _notifications.ShowError("Не удалось зарегистрироваться",
                ex.Kind == ErrorKind.Network ? "Нет связи с сервером" : ex.Message);
        }
    }

    [RelayCommand]
    private void Cancel() => _dialogs.Close(this);
}
