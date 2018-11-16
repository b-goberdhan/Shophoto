using Shophoto.Views.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Views
{
    public class MainViewVM
    {
        public MainViewVM(SideMenuVM sideMenuVM)
        {

        }

        public bool OnCollectionView
        {
            get
            {
                return true;
            }
        }
    }
}
