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
        private readonly Predicate<object> _canExecute;


        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
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
            _execute(parameter);
        }
    }
}