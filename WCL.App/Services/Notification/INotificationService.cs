namespace WCL.App.Services.Notification;

public interface INotificationService
{
    public void ShowSuccess(string title, string? message = null);
    public void ShowError(string title, string? message = null);
}
