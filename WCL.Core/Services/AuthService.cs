using WCL.Core.Abstractions;
using WCL.Core.Errors;
using WCL.Core.Models.Requests;

namespace WCL.Core.Services;

public sealed class AuthService(IAuthApi api, ISessionStore session) : IAuthService
{
    public async Task LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        session.SetTokens(await api.LoginAsync(request, ct));
        try
        {
            session.SignIn(await api.GetCurrentUserAsync(ct));
        }
        catch
        {
            session.SignOut();
            throw;
        }
    }

    public async Task RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await api.RegisterAsync(request, ct);
        try
        {
            await LoginAsync(new LoginRequest(request.Email, request.Password), ct);
        }
        catch (ServiceException ex)
        {
            throw new ServiceException(ErrorKind.AutoLoginFailed,
                "Аккаунт создан, но войти автоматически не удалось", ex);
        }
    }

    public async Task LogoutAsync(CancellationToken ct = default)
    {
        string? refreshToken = session.Tokens?.RefreshToken;
        try
        {
            if (refreshToken is not null)
                await api.LogoutAsync(refreshToken, ct);
        }
        catch (ServiceException)
        {
            // ingore
        }
        finally
        {
            session.SignOut();
        }
    }
}
