using CommunityToolkit.Mvvm.ComponentModel;
using ShadUI;
using WCL.App.Views.Auth;

namespace WCL.App.Views.Main;

public sealed partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public partial SectionViewModel SelectedSection { get; set; }

    public MainViewModel(
        IEnumerable<SectionViewModel> sections,
        AuthViewModel auth,
        DialogManager dialogManager,
        ToastManager toastManager)
    {
        Sections = [.. sections];
        SelectedSection = Sections[0];
        Auth = auth;
        DialogManager = dialogManager;
        ToastManager = toastManager;
    }

    public IReadOnlyList<SectionViewModel> Sections { get; }
    public AuthViewModel Auth { get; }
    public DialogManager DialogManager { get; }
    public ToastManager ToastManager { get; }
}
