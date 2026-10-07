namespace WCL.Api.Dtos;

internal sealed record LoginResponseDto(string AccessToken, string RefreshToken, int ExpiresIn);
