namespace WCL.Core.Models;

public sealed record User(
    string Id,
    string Email,
    string Name,
    string Role,
    string? AvatarUrl);
