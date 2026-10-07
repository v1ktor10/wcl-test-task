namespace WCL.Core.Errors;

public sealed class ServiceException(ErrorKind kind, string message, Exception? inner = null) : Exception(message, inner)
{
    public ErrorKind Kind { get; } = kind;
}
