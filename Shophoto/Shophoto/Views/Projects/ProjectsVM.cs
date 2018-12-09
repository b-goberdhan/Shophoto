using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.Menus.Context;
using Shophoto.Services;
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
using System.Windows.Input;

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
            SearchBoxVM searchBoxVM,
            ProjectService projectService)
        {
            ProjectsFABButtonVM = fABPlusButtonVM;
            CreateProjectVM = createProjectVM;
            SortDropdownMenuVM = sortDropdownMenuVM;
            SortDropdownMenuVM.SortDateText = "Date Created";
            SearchBoxVM = searchBoxVM;
            SearchBoxVM.PlaceHolderText = "Search by name";
            SearchBoxVM.HasErrorMessage = true;
            SearchBoxVM.ErrorMessage = "No search results";
            ProjectService = projectService;
            RegisterEvents();
        }


        private void RegisterEvents()
        {
            ProjectsFABButtonVM.OnAddProjectClicked += ProjectsFABButtonVM_OnAddProjectClicked;
            CreateProjectVM.OnGoBackClicked += CreateProjectVM_OnGoBackClicked;
            CreateProjectVM.OnCreateProjectClicked += CreateProjectVM_OnCreateProjectClicked;
            SortDropdownMenuVM.PropertyChanged += SortDropdownMenuVM_PropertyChanged;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            ProjectService.PropertyChanged += ProjectService_PropertyChanged;
            foreach (ProjectFolderVM folder in ProjectService.Projects)
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

        private ICommand _addProjectCommand; 
        public ICommand AddProjectCommand
        {
            get
            {
                return _addProjectCommand ?? (_addProjectCommand = new CommandHandler(() =>
                {
                    State = ProjectsPageState.CreateProjectPage;
                }));
            }
        }

        public CreateProjectVM CreateProjectVM { get; }
        private void CreateProjectVM_OnGoBackClicked(object sender, EventArgs e)
        {
            State = ProjectsPageState.ProjectsPage;
            NotifyPropertyChanged("HasProjects");
            CreateProjectVM.Reset();

        }
        private void CreateProjectVM_OnCreateProjectClicked(ProjectFolderVM projectFolderVM)
        {
            ProjectService.Projects.Add(projectFolderVM);
            projectFolderVM.OnProjectFolderOpen += Folder_OnProjectFolderOpen;
            CreateProjectVM.Reset();
            State = ProjectsPageState.ProjectsPage;
            NotifyPropertyChanged("HasProjects");
            SortProjects();
        }

        public SortDropdownMenuVM SortDropdownMenuVM { get; }
        private void SortDropdownMenuVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SortProjects();
        }

        private void SortProjects()
        {
            if (SortDropdownMenuVM.SortingState == SortDropdownState.Alphabetical)
            {
                ProjectService.SortByName();
                
            }
            else if (SortDropdownMenuVM.SortingState == SortDropdownState.Date)
            {
                ProjectService.SortByDateCreated();
            }
        }

        public SearchBoxVM SearchBoxVM { get; }
        private void SearchBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InputText")
            {
                FilterBySearchText();
            }
        }

        private void FilterBySearchText()
        {
            int visibleImages = 0;
            foreach (var project in ProjectService.Projects)
            {

                project.IsVisible = project.Name.ToLower().IndexOf(SearchBoxVM.InputText.ToLower()) == 0;
                if (project.IsVisible)
                {
                    visibleImages++;
                }
            }
            SearchBoxVM.HasError = visibleImages == 0 && SearchBoxVM.InputText != "";
        }
        private void ApplyAllFilters()
        {
            //Sort
            //Filter
        }

        private void Folder_OnProjectFolderOpen(object sender, EventArgs e)
        {
            var projectFolder = (ProjectFolderVM)sender;
            ProjectService.SetCurrentlyOpenedProject(projectFolder, GoBackToProjectsDirectory);
            ProjectsFABButtonVM.IsOpen = false;
        }


        public void GoBackToProjectsDirectory()
        {

            ProjectService.SetCurrentlyOpenedProject(null, null);
            NotifyPropertyChanged("HasProjects");
        }

        public ProjectService ProjectService { get; }
        private void ProjectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("IsProjectFolderOpen");
        }

        private void CurrentlyOpenedProject_OnGoBackClick(object sender, EventArgs e)
        {
            GoBackToProjectsDirectory();
        }

        public bool IsProjectFolderOpen
        {
            get { return ProjectService.CurrentlyOpenedProject != null; }
        }

        public bool HasProjects
        {
            get { return ProjectService.Projects.Count > 0; }
        }

    }
}
