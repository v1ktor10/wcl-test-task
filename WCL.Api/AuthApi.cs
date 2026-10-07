using System.Net.Http.Json;
using WCL.Api.Dtos;
using WCL.Core.Abstractions;
using WCL.Core.Errors;
using WCL.Core.Models;
using WCL.Core.Models.Requests;

namespace WCL.Api;

internal sealed class AuthApi(HttpClient http) : IAuthApi
{
    public Task RegisterAsync(RegisterRequest request, CancellationToken ct = default) =>
        SendAsync(HttpMethod.Post, "api/auth/register",
            new RegisterRequestDto(request.Email, request.Name, request.Password), ct);

    public async Task<AuthTokens> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var dto = await SendAsync<LoginResponseDto>(HttpMethod.Post, "api/auth/login",
            new LoginRequestDto(request.Email, request.Password), ct);

        return new AuthTokens(dto.AccessToken, dto.RefreshToken);
    }

    public async Task<User> GetCurrentUserAsync(CancellationToken ct = default)
    {
        var dto = await SendAsync<UserDto>(HttpMethod.Get, "api/auth/me", null, ct);
        return new User(dto.Id, dto.Email, dto.Name, dto.Role, dto.Avatar);
    }

    public Task LogoutAsync(string refreshToken, CancellationToken ct = default) =>
        SendAsync(HttpMethod.Post, "api/auth/logout", new LogoutRequestDto(refreshToken), ct);

    private async Task SendAsync(HttpMethod method, string url, object? body, CancellationToken ct)
    {
        using var response = await SendCoreAsync(method, url, body, ct);
    }

    private async Task<T> SendAsync<T>(HttpMethod method, string url, object? body, CancellationToken ct)
    {
        using var response = await SendCoreAsync(method, url, body, ct);
        return await response.Content.ReadFromJsonAsync<T>(ApiJson.Options, ct)
               ?? throw new ServiceException(ErrorKind.Unknown, "Пустой ответ сервера");
    }

    private async Task<HttpResponseMessage> SendCoreAsync(
        HttpMethod method, string url, object? body, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(method, url);
        if (body is not null)
            request.Content = JsonContent.Create(body, body.GetType(), options: ApiJson.Options);

        HttpResponseMessage response;
        try
        {
            response = await http.SendAsync(request, ct);
        }
        catch (HttpRequestException ex)
        {
            throw new ServiceException(ErrorKind.Network, "No connection to the server", ex);
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            throw new ServiceException(ErrorKind.Network, "Timeout", ex);
        }

        if (response.IsSuccessStatusCode) return response;

        using (response)
            throw await ErrorMapper.ToExceptionAsync(response, ct);
    }
}
