using Shophoto.Buttons;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.AuxView;
using Shophoto.Views.Projects.Folder;
using Shophoto.Views.Projects.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            CreateProjectVM createProjectVM,
            SortDropdownMenuVM sortDropdownMenuVM,
            SearchBoxVM searchBoxVM)
        {
            ProjectsFABButtonVM = fABPlusButtonVM;
            CreateProjectVM = createProjectVM;
            SortDropdownMenuVM = sortDropdownMenuVM;
            SearchBoxVM = searchBoxVM;
            ProjectFolders = new ObservableCollection<ProjectFolderVM>();
            
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project1" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project2" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project3" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project4" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project5" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project6" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project7" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project8" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project9" });
            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project10" });

            RegisterEvents();
        }


        private void RegisterEvents()
        {
            ProjectsFABButtonVM.OnAddProjectClicked += ProjectsFABButtonVM_OnAddProjectClicked;
            CreateProjectVM.OnGoBackClicked += CreateProjectVM_OnGoBackClicked;
            CreateProjectVM.OnCreateProjectClicked += CreateProjectVM_OnCreateProjectClicked;
            SortDropdownMenuVM.PropertyChanged += SortDropdownMenuVM_PropertyChanged;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            foreach (ProjectFolderVM folder in ProjectFolders)
            {
                folder.OnProjectFolderOpen += Folder_OnProjectFolderOpen;
            }
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
        private void CreateProjectVM_OnCreateProjectClicked(ProjectFolderVM projectFolderVM)
        {
            ProjectFolders.Add(projectFolderVM);
            projectFolderVM.OnProjectFolderOpen += Folder_OnProjectFolderOpen;
            CreateProjectVM.Reset();
            State = ProjectsPageState.ProjectsPage;
        }

        public SortDropdownMenuVM SortDropdownMenuVM { get; }
        private void SortDropdownMenuVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public SearchBoxVM SearchBoxVM { get; }
        private void SearchBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
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
            var projectFolder = (ProjectFolderVM)sender;
            CurrentlyOpenedProject = new ProjectVM()
            {
                Name = projectFolder.Name,
                CollectionsVM = projectFolder.CollectionsVM,
                Summary = projectFolder.Summary,
                CustomerName = projectFolder.CustomerName,
                CustomerEmail = projectFolder.CustomerEmail
            };
        }

        public void GoBackToProjectsDirectory()
        {
            CurrentlyOpenedProject = null;
        }

        private ProjectVM _currenltyOpenedProject;
        public ProjectVM CurrentlyOpenedProject
        {
            get { return _currenltyOpenedProject; }
            private set
            {
                _currenltyOpenedProject = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsProjectFolderOpen");
            }
        }

        public bool IsProjectFolderOpen
        {
            get { return CurrentlyOpenedProject != null; }
        }

    }
}
