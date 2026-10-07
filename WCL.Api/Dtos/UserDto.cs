namespace WCL.Api.Dtos;

internal sealed record UserDto(string Id, string Email, string Name, string Role, string? Avatar);
