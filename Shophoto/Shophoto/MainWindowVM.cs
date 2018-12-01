using Shophoto.Menus;
using Shophoto.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.ViewModels
{
    public class MainWindowVM
    {
        public MainWindowVM(
            MainViewVM mainViewVM,
            SideMenuVM sideMenuVM)
        {
            MainViewVM = mainViewVM;
            SideMenuVM = sideMenuVM;
        }

        public MainViewVM MainViewVM { get; }

        public SideMenuVM SideMenuVM { get; }

        public void OnCollectionButtonClicked()
        {

        }
    }
}
