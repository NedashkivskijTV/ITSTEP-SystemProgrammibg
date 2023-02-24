using System;
using System.Windows.Input;

namespace TPLDemo.ViewModel
{
    class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> action, Predicate<object> predicate)
        {
            ExecuteDelegate = action;
            CanExecuteDelegate = predicate;
        }

        public RelayCommand(Action<object> action)
        {
            ExecuteDelegate = action;
            CanExecuteDelegate = (t) => true;
        }

        protected Predicate<object> CanExecuteDelegate { get; set; }
        protected Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
    }
}
