## Архитектура

```
WCL.Desktop  ──►( WCL.App  ──►  WCL.Core  ◄──  WCL.Api )
 (точка входа,     (View +        (модели,        (HTTP-клиенты,
  DI-контейнер)    ViewModel,      интерфейсы,     DTO, маппинг
                   UI-сервисы)     ошибки)         ошибок)
```

| Проект | Ответственность | Зависит от |
|---|---|---|
| **WCL.Core** | Доменные модели, контракты | — |
| **WCL.Api** | Реализация контрактов поверх `HttpClient`, DTO, преобразование HTTP-ошибок в `ServiceException`| Core |
| **WCL.App** | Views, ViewModels, UI-сервисы | Core |
| **WCL.Desktop** | Composition root: собирает DI-контейнер, запускает приложение | Api, App, Core |
