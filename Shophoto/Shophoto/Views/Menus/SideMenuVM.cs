using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Menus
{
    public class SideMenuVM
    {
        private readonly MainWindowVM _mainVM;
        public SideMenuVM(MainWindowVM mainVM)
        {
            _mainVM = mainVM;
        }

        public ICommand _collectionButtonCommand;
        public ICommand CollectionButtonCommand
        {
            get {
                return _collectionButtonCommand ?? (_collectionButtonCommand = new CommandHandler(_mainVM.OnCollectionButtonClicked));
            }
        }
    }
}
