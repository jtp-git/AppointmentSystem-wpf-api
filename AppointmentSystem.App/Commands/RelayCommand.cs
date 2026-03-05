
using System.Windows.Input;

namespace AppointmentSystem.App.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object?, Task>? _canExecute;
        public RelayCommand(Func<object?, Task>? canExecute)
        {
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => true;
        public async void Execute(object? parameter)
        {
            if (_canExecute != null)
            {
                await _canExecute(parameter);
            }
        }
    }
}
