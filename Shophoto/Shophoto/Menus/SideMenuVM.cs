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
        Collection
    }
    public class SideMenuVM : BaseVM
    {
        public SideMenuVM()
        {
            State = SideMenuState.Project;
        }

        public ICommand _projectsButtonClicked;
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
            }
        }

        public bool IsProjectSelected
        {
            get { return State == SideMenuState.Project; }
        }
    }
}
