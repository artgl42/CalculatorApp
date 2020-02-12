using System;
using System.Windows.Input;

namespace CalculatorApp.Toolbox
{
    public class DelegateCommand : ICommand
    {
        readonly private Action<object> _executeCommand;
        readonly private Predicate<object> _canExecuteCommand;

        public DelegateCommand(Action<object> executeCommand, Predicate<object> canExecuteCommand)
        {
            _executeCommand = executeCommand ?? throw new ArgumentNullException("executeCommand");
            _canExecuteCommand = canExecuteCommand;
        }

        public DelegateCommand(Action<object> executeCommand) : this(executeCommand, null) { }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteCommand?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _executeCommand(parameter);
        }
    }
}
