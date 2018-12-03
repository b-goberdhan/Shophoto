using Shophoto.Command;
using Shophoto.InputBox;
using Shophoto.ViewModels;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.AuxView
{
    public delegate void CreateProjectFolderHandler(ProjectFolderVM projectFolderVM);
    public class CreateProjectVM : BaseVM
    {
        public event EventHandler OnGoBackClicked;
        public event CreateProjectFolderHandler OnCreateProjectClicked;
        public CreateProjectVM(
            InputBoxVM projectNameVM,
            LargeInputBoxVM projectSummaryInputBoxVM,
            InputBoxVM customerNameInputBoxVM,
            InputBoxVM emailInputBoxVM)
        {
            ProjectNameVM = projectNameVM;
            ProjectSummaryInputBoxVM = projectSummaryInputBoxVM;
            CustomerNameInputBoxVM = customerNameInputBoxVM;
            EmailInputBoxVM = emailInputBoxVM;
            RegisterEvents();    
        }



        private void RegisterEvents()
        {
            ProjectNameVM.PropertyChanged += ProjectNameVM_PropertyChanged;
            ProjectSummaryInputBoxVM.PropertyChanged += LargeInputBoxVM_PropertyChanged;
            CustomerNameInputBoxVM.PropertyChanged += CustomerNameInputBoxVM_PropertyChanged;
            EmailInputBoxVM.PropertyChanged += EmailInputBoxVM_PropertyChanged;
        }

        public InputBoxVM ProjectNameVM { get; }
        private void ProjectNameVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }


        public LargeInputBoxVM ProjectSummaryInputBoxVM { get; }
        private void LargeInputBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanCreateProject = VerifyInputBoxesHaveText();
        }

        public InputBoxVM CustomerNameInputBoxVM { get; }
        private void CustomerNameInputBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanCreateProject = VerifyInputBoxesHaveText();
        }

        public InputBoxVM EmailInputBoxVM { get; }
        private void EmailInputBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanCreateProject = VerifyInputBoxesHaveText();
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new CommandHandler(() =>
                {
                    OnGoBackClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _createProjectCommand;
        public ICommand CreateProjectCommand
        {
            get
            {
                return _createProjectCommand ?? (_createProjectCommand = new CommandHandler(() =>
                {
                    //Ensure that all inputs were provided properly before
                    //this event.


                    if (!VerifyInputBoxesHaveText())
                    {
                        SetErrorOnInputBoxes();
                    }
                    else
                    {

                        
                        OnCreateProjectClicked?.Invoke(null);
                    }                   
                }));
            }
        }

        private bool _canCreateProject;
        public bool CanCreateProject
        {
            get { return _canCreateProject; }
            set
            {
                _canCreateProject = value;
                NotifyPropertyChanged();
            }
        }

        private bool VerifyInputBoxesHaveText()
        {
            return 
                ProjectSummaryInputBoxVM.InputText.Replace(" ", "") != "" ||
                CustomerNameInputBoxVM.InputText.Replace(" ", "") != "" ||
                EmailInputBoxVM.InputText.Replace(" ", "") != "";
        }

        private void SetErrorOnInputBoxes()
        {
            ProjectSummaryInputBoxVM.HasError = ProjectSummaryInputBoxVM.InputText.Replace(" ", "") == "";
            CustomerNameInputBoxVM.HasError = CustomerNameInputBoxVM.InputText.Replace(" ", "") == "";
            EmailInputBoxVM.HasError = EmailInputBoxVM.InputText.Replace(" ", "") == "";
        }
    }
}
