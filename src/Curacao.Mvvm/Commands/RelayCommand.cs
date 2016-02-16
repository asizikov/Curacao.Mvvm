﻿using System;
using System.Diagnostics;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Commands
{
    [PublicAPI]
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Action _executeVoid;
        private readonly Predicate<object> _canExecute;


        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _executeVoid = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        [PublicAPI]
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute(parameter);
                return;
            }
            if (_executeVoid != null)
            {
                _executeVoid();
            }
            
        }
    }
}
