using Shophoto.Command;
using Shophoto.InputBox;
using Shophoto.Services;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.AuxView
{
    public class DeleteProjectVM : BaseVM
    {

        public event EventHandler OnDelete;

        public DeleteProjectVM(ProjectService projectService)
        {
            ProjectNameInputBoxVM = new InputBoxVM()
            {
                PlaceHolderText = "Name of project to be deleted"
            };
            ProjectService = projectService;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ProjectNameInputBoxVM.PropertyChanged += ProjectNameInputBoxVM_PropertyChanged;
        }

        private void ProjectNameInputBoxVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                CanDelete = ProjectNameInputBoxVM.InputText == ProjectService.CurrentlyOpenedProject.Name;
            }

        }

        public InputBoxVM ProjectNameInputBoxVM { get; }
        public ProjectService ProjectService { get; }

        private bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                _canDelete = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _onDeleteCommand;
        public ICommand OnDeleteCommand
        {
            get
            {
                return _onDeleteCommand ?? (_onDeleteCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                    OnDelete?.Invoke(this, null);
                    ProjectNameInputBoxVM.Reset();
                }));
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                    ProjectNameInputBoxVM.Reset();
                }));
            }
        }
    }
}
