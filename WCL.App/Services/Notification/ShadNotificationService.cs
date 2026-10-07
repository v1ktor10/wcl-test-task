using ShadUI;

namespace WCL.App.Services.Notification;

internal sealed class ShadNotificationService(ToastManager toasts) : INotificationService
{
    public void ShowSuccess(string title, string? message = null) =>
        toasts.CreateToast(title).WithDelay(3).WithContent(message ?? "").DismissOnClick().ShowSuccess();

    public void ShowError(string title, string? message = null) =>
        toasts.CreateToast(title).WithDelay(3).WithContent(message ?? "").DismissOnClick().ShowError();
}
