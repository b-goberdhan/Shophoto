using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Menus.Context
{
    public abstract class ContextMenuVM : BaseVM
    {

        public ContextMenuVM()
        {
            MenuItems = new ObservableCollection<ContextMenuItemVM>();
        }

        private ObservableCollection<ContextMenuItemVM> _contextMenuItems;
        public ObservableCollection<ContextMenuItemVM> MenuItems
        {
            get { return _contextMenuItems; }
            set
            {
                _contextMenuItems = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isClosed = false;
        public bool IsOpen
        {
            get { return _isClosed; }
            set
            {
                _isClosed = value;
                NotifyPropertyChanged();
            }
        }

    }
}
