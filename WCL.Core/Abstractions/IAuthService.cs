using WCL.Core.Models.Requests;

namespace WCL.Core.Abstractions;

public interface IAuthService
{
    public Task LoginAsync(LoginRequest request, CancellationToken ct = default);
    public Task RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    public Task LogoutAsync(CancellationToken ct = default);
}
