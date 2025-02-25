using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace TrackDesigner.Util;

public class RelayCommand : ICommand
{
    [NotNull]
    private readonly Action _execute;
    [NotNull]
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public RelayCommand(Action execute)
        : this(execute, () => true)
    { }

    public bool CanExecute([NotNull] object parameter) => _canExecute();

    public void Execute([NotNull] object parameter) => _execute();

    public event EventHandler CanExecuteChanged = (sender, args) => { };

    // ReSharper disable once UnusedMember.Global
    public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);
}