using WCL.Core.Models;
using WCL.Core.Models.Requests;

namespace WCL.Core.Abstractions;

public interface ISessionStore : ISessionState
{
    public AuthTokens? Tokens { get; }
    public void SetTokens(AuthTokens tokens);
    public void SignIn(User user);
    public void SignOut();
}
