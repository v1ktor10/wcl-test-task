using WCL.Core.Abstractions;
using WCL.Core.Models;

namespace WCL.App.Views.Profile;

public sealed class ProfileViewModel : SectionViewModel
{
    private readonly ISessionState _session;

    public ProfileViewModel(ISessionState session)
    {
        _session = session;
        session.Changed += (_, _) =>
        {
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(User));
        };
    }

    public override string Title => "Профиль";
    public bool IsAuthenticated => _session.IsAuthenticated;
    public User? User => _session.CurrentUser;
}
