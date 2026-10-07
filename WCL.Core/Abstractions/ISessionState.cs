using WCL.Core.Models;

namespace WCL.Core.Abstractions;

public interface ISessionState
{
    public User? CurrentUser { get; }
    public bool IsAuthenticated { get; }
    public event EventHandler? Changed;
}
