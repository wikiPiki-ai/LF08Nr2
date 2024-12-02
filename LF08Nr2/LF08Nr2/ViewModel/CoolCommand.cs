using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LF08Nr2.ViewModel
{
    class CoolCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _action;

        private Func<object?, bool>? _canExecute;

        public CoolCommand(Action<object> action) 
        {
            _action = action;
        }

        public CoolCommand(Action<object?> action, Func<object?, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}
