using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;

namespace WeatherChecker.ViewModel.Helper
{
    public class Command : ICommand
    {
        private readonly Delegate _toDoDelegate;

        public Command(Delegate iLlDoItDelegate)
        {
            _toDoDelegate = iLlDoItDelegate;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _toDoDelegate.DynamicInvoke();
        }
        public event EventHandler CanExecuteChanged;
    }
}
