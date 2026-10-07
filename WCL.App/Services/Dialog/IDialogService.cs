namespace WCL.App.Services.Dialog;

public interface IDialogService
{
    public void Show<TViewModel>() where TViewModel : class;

    public void Confirm(string title, string message, Action onConfirm,
        string confirmText = "OK", string cancelText = "Отмена");

    public void Close<TViewModel>(TViewModel viewModel) where TViewModel : class;
}
