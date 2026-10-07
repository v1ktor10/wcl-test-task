using WCL.Core.Abstractions;
using WCL.Core.Models;
using WCL.Core.Models.Requests;

namespace WCL.Core.Services;

public sealed class Session : ISessionStore
{
    public AuthTokens? Tokens { get; private set; }
    public User? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;
    public event EventHandler? Changed;

    public void SetTokens(AuthTokens tokens) => Tokens = tokens;

    public void SignIn(User user)
    {
        CurrentUser = user;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void SignOut()
    {
        Tokens = null;
        CurrentUser = null;
        Changed?.Invoke(this, EventArgs.Empty);
    }
}
