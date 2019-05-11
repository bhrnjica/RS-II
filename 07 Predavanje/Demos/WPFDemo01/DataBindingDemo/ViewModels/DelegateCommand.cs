using System;
using System.Windows.Input;

namespace DataBindingDemo.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action m_Action;

        public DelegateCommand(Action action)
        {
            m_Action = action;
        }

        public void Execute(object parameter)
        {
            m_Action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
