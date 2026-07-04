using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchemeEditorUI
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Predicate<object?> _canExecute;
        private Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object?> canExecute) 
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
