using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Menus.Context
{
    public class ContextMenuItemVM : BaseVM
    {

        private readonly Action _action;
        public ContextMenuItemVM(string name, Action action)
        {
            _action = action;
            _name = name;
        }


        private ICommand _onClick;
        public ICommand OnClick
        {
            get
            {
                return _onClick ?? (_onClick = new CommandHandler(() =>
                {
                    _action();
                }));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        
         
    }
}
