using System.Windows.Input;

namespace questions_WPF.Command;

public sealed class MyICommand : ICommand
{
    private readonly Action<object?> executeMethod;

    public event EventHandler? CanExecuteChanged;

    public MyICommand(Action<object?> executeMethod)
    {
        this.executeMethod = executeMethod;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        this.executeMethod(parameter);
    }
}

