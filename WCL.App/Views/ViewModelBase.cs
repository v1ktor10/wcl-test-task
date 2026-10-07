using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WCL.App.Views;

public abstract partial class ViewModelBase : ObservableValidator
{
    private readonly List<IAsyncRelayCommand> _trackedCommands = [];

    [ObservableProperty] public partial bool IsLoading { get; private set; }

    protected void TrackLoading(params IAsyncRelayCommand[] commands)
    {
        foreach (var command in commands)
        {
            _trackedCommands.Add(command);
            command.PropertyChanged += OnCommandPropertyChanged;
        }
    }

    private void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAsyncRelayCommand.IsRunning))
            IsLoading = _trackedCommands.Any(c => c.IsRunning);
    }
}
