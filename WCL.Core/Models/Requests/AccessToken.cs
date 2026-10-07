namespace WCL.Core.Models.Requests;

public sealed record AuthTokens(string AccessToken, string RefreshToken, DateTimeOffset ExpiresAt);
