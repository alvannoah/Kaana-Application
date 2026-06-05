using System;
using System.Threading.Tasks;
using System.Windows.Input;

public class RelayCommand<T> : ICommand
{
    private readonly Func<T, Task> _execute;

    public RelayCommand(Func<T, Task> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object parameter) => true;

    public async void Execute(object parameter)
    {
        await _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged;
}