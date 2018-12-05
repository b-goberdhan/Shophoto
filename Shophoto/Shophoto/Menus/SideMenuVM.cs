using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Menus
{
    public enum SideMenuState
    {
        Project,
        AllImages
    }
    public class SideMenuVM : BaseVM
    {
        public SideMenuVM()
        {
            State = SideMenuState.Project;
        }

        private ICommand _allImagesButtonClickCommand;
        public ICommand AllImagesButtonClickCommand
        {
            get
            {
                return _allImagesButtonClickCommand ?? (_allImagesButtonClickCommand = new CommandHandler(() =>
                {
                    State = SideMenuState.AllImages;
                }));
            }
        }

        private ICommand _projectsButtonClicked;
        public ICommand ProjectsButtonClickCommand
        {
            get {
                return _projectsButtonClicked ?? (_projectsButtonClicked = new CommandHandler(() =>
                {
                    State = SideMenuState.Project;
                }));
            }
        }

        private SideMenuState _state;
        public SideMenuState State
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsProjectSelected");
                NotifyPropertyChanged("IsAllImagesSelected");
            }
        }

        public bool IsProjectSelected
        {
            get { return State == SideMenuState.Project; }
        }

        public bool IsAllImagesSelected
        {
            get { return State == SideMenuState.AllImages; }
        }
    }
}
