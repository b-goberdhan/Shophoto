using Shophoto.Buttons;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.AuxView;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Views.Projects
{
    public enum ProjectsPageState
    {
        ProjectsPage,
        CreateProjectPage
    }
    public class ProjectsVM : BaseVM
    {

        public ProjectsVM(
            ProjectsFABButtonVM fABPlusButtonVM,
            CreateProjectVM createProjectVM)
        {
            ProjectsFABButtonVM = fABPlusButtonVM;
            CreateProjectVM = createProjectVM;
            ProjectFolders = new ObservableCollection<ProjectFolderVM>();

            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project" });
            RegisterEvents();
        }

        private ProjectsPageState _state;
        public ProjectsPageState State
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsOnProjectPage");
                NotifyPropertyChanged("IsOnCreateProjectPage");
            }
        }

        public bool IsOnProjectPage
        {
            get { return State == ProjectsPageState.ProjectsPage; }
        }

        public bool IsOnCreateProjectPage
        {
            get { return State == ProjectsPageState.CreateProjectPage; }
        }


        private void RegisterEvents()
        {
            ProjectsFABButtonVM.OnAddProjectClicked += ProjectsFABButtonVM_OnAddProjectClicked;
            CreateProjectVM.OnGoBackClicked += CreateProjectVM_OnGoBackClicked;
            foreach (ProjectFolderVM folder in ProjectFolders)
            {
                folder.OnProjectFolderOpen += Folder_OnProjectFolderOpen;
            }
        }

        

        public ProjectsFABButtonVM ProjectsFABButtonVM { get; }
        private void ProjectsFABButtonVM_OnAddProjectClicked(object sender, EventArgs e)
        {
            State = ProjectsPageState.CreateProjectPage;
        }

        public CreateProjectVM CreateProjectVM { get; }
        private void CreateProjectVM_OnGoBackClicked(object sender, EventArgs e)
        {
            State = ProjectsPageState.ProjectsPage;
        }



        private ObservableCollection<ProjectFolderVM> _projectFolders;
        public ObservableCollection<ProjectFolderVM> ProjectFolders
        {
            get { return _projectFolders; }
            set
            {
                _projectFolders = value;
                NotifyPropertyChanged();
            }
        }

        private void Folder_OnProjectFolderOpen(object sender, EventArgs e)
        {
            var project = (ProjectFolderVM)sender;
            CurrentlyOpenedProjectFolder = project;
        }

        private ProjectFolderVM _currenltyOpenedProjectFolder;
        public ProjectFolderVM CurrentlyOpenedProjectFolder
        {
            get { return _currenltyOpenedProjectFolder; }
            set
            {
                _currenltyOpenedProjectFolder = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsProjectFolderOpen");
            }
        }

        public bool IsProjectFolderOpen
        {
            get { return CurrentlyOpenedProjectFolder != null; }
        }

    }
}
