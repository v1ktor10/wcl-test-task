using WCL.Core.Models;
using WCL.Core.Models.Requests;

namespace WCL.Core.Abstractions;

public interface IAuthApi
{
    public Task RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    public Task<AuthTokens> LoginAsync(LoginRequest request, CancellationToken ct = default);
    public Task<User> GetCurrentUserAsync(CancellationToken ct = default);
    public Task LogoutAsync(string refreshToken, CancellationToken ct = default);
}
