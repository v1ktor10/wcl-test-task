using Microsoft.Extensions.DependencyInjection;
using ShadUI;

namespace WCL.App.Services.Dialog;

internal sealed class ShadDialogService(DialogManager dialogs, IServiceProvider services) : IDialogService
{
    public void Show<TViewModel>() where TViewModel : class
    {
        var viewModel = services.GetRequiredService<TViewModel>();

        dialogs.CreateDialog(viewModel)
            .WithMinWidth(380)
            .Dismissible()
            .Show();
    }

    public void Confirm(string title, string message, Action onConfirm,
        string confirmText = "OK", string cancelText = "Отмена") =>
        dialogs.CreateDialog(title, message)
            .WithPrimaryButton(confirmText, onConfirm)
            .WithCancelButton(cancelText)
            .WithMinWidth(300)
            .Show();

    public void Close<TViewModel>(TViewModel viewModel) where TViewModel : class =>
        dialogs.Close(viewModel);
}
