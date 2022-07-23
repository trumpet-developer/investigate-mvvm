using System;
using System.Windows.Input;

namespace a.Mvvm.Organic
{
    internal class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action executeMethod;
        private Func<bool> canExecuteMethod;

        public Command(Action executeMethod) :
            this(executeMethod, () => true)
        {
        }

        public Command(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
            this.canExecuteMethod = canExecuteMethod ?? throw new ArgumentNullException(nameof(canExecuteMethod));
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            executeMethod();
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
