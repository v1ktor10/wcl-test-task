namespace WCL.App.Views.About;

public sealed class AboutViewModel : SectionViewModel
{
    public override string Title => "О разработчике";

    public string Name => "Десятов Виктор";

    public string Role => "Desktop Developer";

    public string Stack => ".NET, Avalonia, WPF, MVVM, ReactiveUI";

    public string Contacts => "@v10ctor";
}
