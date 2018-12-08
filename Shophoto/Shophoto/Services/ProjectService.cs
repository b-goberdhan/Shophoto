using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Projects.Folder;
using Shophoto.Views.Projects.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Services
{
    public class ProjectService : BaseVM
    {
        public ProjectService()
        {
            Projects = new ObservableCollection<ProjectFolderVM>();
        }

        private ObservableCollection<ProjectFolderVM> _projects;
        public ObservableCollection<ProjectFolderVM> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyPropertyChanged();
            }
        }

        public ProjectVM CurrentlyOpenedProject { get; private set; }
        private Action _goBackClickHandler;
        private void _currentlyOpenedProject_OnGoBackClick(object sender, EventArgs e)
        {
            _goBackClickHandler?.Invoke();
        }

        public void SetCurrentlyOpenedProject(ProjectFolderVM projectFolder, Action goBackClickHandler)
        {

            if (CurrentlyOpenedProject != null)
            {
                CurrentlyOpenedProject.OnGoBackClick -= _currentlyOpenedProject_OnGoBackClick;
            }

            if (projectFolder == null)
            {
                CurrentlyOpenedProject = null;
                _goBackClickHandler = null;
            }
            else
            {
                CurrentlyOpenedProject = new ProjectVM(this, projectFolder)
                {
                    Name = projectFolder.Name,
                    CollectionsVM = projectFolder.CollectionsVM,
                    Summary = projectFolder.Summary,
                    CustomerName = projectFolder.CustomerName,
                    CustomerEmail = projectFolder.CustomerEmail
                };
                CurrentlyOpenedProject.Init();
                _goBackClickHandler = goBackClickHandler;
                CurrentlyOpenedProject.OnGoBackClick += _currentlyOpenedProject_OnGoBackClick;
            }

            NotifyPropertyChanged("CurrentlyOpenedProject");
        }

        public void DeleteCurrentlyOpenedProject()
        {
            //CurrentlyOpenedProject.OnGoBackClick -= _currentlyOpenedProject_OnGoBackClick;
            Projects.Remove(CurrentlyOpenedProject.ProjectFolderVM);
            CurrentlyOpenedProject = null;
        }

        public void SortByName()
        {
            Projects = new ObservableCollection<ProjectFolderVM>(Projects.OrderBy((project) => 
            {
                return project.Name;
            }));
        }

        public void SortByDateCreated()
        {
            Projects = new ObservableCollection<ProjectFolderVM>(Projects.OrderByDescending((project) =>
            {
                return project.DateCreated;
            }));
        }

    }
}
