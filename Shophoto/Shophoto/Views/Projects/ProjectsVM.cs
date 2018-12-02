using Shophoto.Buttons;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.AuxView;
using Shophoto.Views.Projects.Folder;
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
            RegisterEvents();
        }


        private void RegisterEvents()
        {
            ProjectsFABButtonVM.OnAddProjectClicked += ProjectsFABButtonVM_OnAddProjectClicked;
            CreateProjectVM.OnGoBackClicked += CreateProjectVM_OnGoBackClicked;
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
