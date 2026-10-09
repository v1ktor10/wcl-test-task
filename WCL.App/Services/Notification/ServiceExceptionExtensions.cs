using WCL.Core.Errors;

namespace WCL.App.Services.Notification;

internal static class ServiceExceptionExtensions
{
    public static string ToUserMessage(this ServiceException ex) => ex.Kind switch
    {
        ErrorKind.Network => "Нет связи с сервером",
        ErrorKind.Server => "Сервер временно недоступен",
        ErrorKind.Unauthorized => "Неверный логин или пароль",
        ErrorKind.Validation => ex.Message,
        _ => "Что-то пошло не так"
    };
}
