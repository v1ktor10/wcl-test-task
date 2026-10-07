using WCL.Core.Models;
using WCL.Core.Models.Requests;

namespace WCL.Core.Abstractions;

public interface ISessionState
{
    public AuthTokens? Tokens { get; }
    public User? CurrentUser { get; }
    public bool IsAuthenticated { get; }
    public event EventHandler? Changed;
    public void SetTokens(AuthTokens tokens);
    public void SignIn(User user);
    public void SignOut();
}
