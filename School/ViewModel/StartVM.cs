using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;

namespace School.ViewModel
{
    public class StartVM : PropertyChangedBase
    {
        private bool _autorize;
        public bool IsAutorize
        {
            get => _autorize;
            set => SetProperty(ref _autorize, value);
        }
        public StartVM()
        {
            Open = new LightCommand(OpenMethod);
        }

        public ICommand Open { get; }

        private void OpenMethod(object obj)
        {

            IsAutorize = true;
        }
    }
}
