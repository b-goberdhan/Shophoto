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
    public class EditProjectVM : BaseVM
    {
        public EditProjectVM(ProjectService projectService)
        {
            ProjectService = projectService;
            ProjectNameInputBoxVM = new InputBoxVM()
            {
                InputText = ProjectService.CurrentlyOpenedProject.Name,
                HasErrorMessage = true,
                ErrorMessage = ERROR_MESSAGE_CANNOT_BE_EMPTY
                
            };
            ProjectSummaryInputBoxVM = new LargeInputBoxVM()
            {
                InputText = ProjectService.CurrentlyOpenedProject.Summary,
                HasErrorMessage = true,
                ErrorMessage = ERROR_MESSAGE_CANNOT_BE_EMPTY
            };
            CustomerNameInputBoxVM = new InputBoxVM()
            {
                InputText = ProjectService.CurrentlyOpenedProject.CustomerName,
                HasErrorMessage = true,
                ErrorMessage = ERROR_MESSAGE_CANNOT_BE_EMPTY
            };
            EmailInputBoxVM = new InputBoxVM()
            {
                InputText = ProjectService.CurrentlyOpenedProject.CustomerEmail,
                HasErrorMessage = true,
                ErrorMessage = ERROR_MESSAGE_CANNOT_BE_EMPTY
            };
            RegisterEvents();
        }

        public const string ERROR_MESSAGE_CANNOT_BE_EMPTY = "Cannot be empty";

        private void RegisterEvents()
        {
            ProjectNameInputBoxVM.PropertyChanged += ProjectNameInputBoxVM_PropertyChanged;
            CustomerNameInputBoxVM.PropertyChanged += CustomerNameInputBoxVM_PropertyChanged;
            EmailInputBoxVM.PropertyChanged += EmailInputBoxVM_PropertyChanged;
        }

        private void EmailInputBoxVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "HasError")
            {
                EmailInputBoxVM.HasError = EmailInputBoxVM.InputText == "";
            }

        }

        private void CustomerNameInputBoxVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "HasError")
            {
                CustomerNameInputBoxVM.HasError = CustomerNameInputBoxVM.InputText == "";
            }
        }


        private void ProjectNameInputBoxVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "HasError")
            {
                ProjectNameInputBoxVM.HasError = ProjectNameInputBoxVM.InputText == "";
            }
        }

        public ProjectService ProjectService { get; }

        public InputBoxVM ProjectNameInputBoxVM { get; private set; }
        public LargeInputBoxVM ProjectSummaryInputBoxVM { get; private set; }
        public InputBoxVM CustomerNameInputBoxVM { get; private set; }
        public InputBoxVM EmailInputBoxVM { get; private set; }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                }));
            }
        }

        private ICommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new CommandHandler(() =>
                {
                    ProjectService.CurrentlyOpenedProject.Name = ProjectNameInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.Summary = ProjectSummaryInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.CustomerEmail = EmailInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.CustomerName = CustomerNameInputBoxVM.InputText;

                    ProjectService.CurrentlyOpenedProject.ProjectFolderVM.Name = ProjectNameInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.ProjectFolderVM.Summary = ProjectSummaryInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.ProjectFolderVM.CustomerName = EmailInputBoxVM.InputText;
                    ProjectService.CurrentlyOpenedProject.ProjectFolderVM.CustomerName = CustomerNameInputBoxVM.InputText;
                    IsVisible = false;
                }));
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
    }
}
