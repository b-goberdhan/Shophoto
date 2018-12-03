using Shophoto.Menus;
using Shophoto.Views;
using Shophoto.Views.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            SideMenuVM.PropertyChanged += SideMenuVM_PropertyChanged;
        }



        public MainViewVM MainViewVM { get; }

        public SideMenuVM SideMenuVM { get; }
        private void SideMenuVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                if (SideMenuVM.State == SideMenuState.Project)
                {
                    MainViewVM.ProjectsVM.GoBackToProjectsDirectory();
                }
            }
        }
    }
}
