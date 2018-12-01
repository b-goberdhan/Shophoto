using Shophoto.Buttons;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Views.Projects
{
    public class ProjectsVM : BaseVM
    {

        public ProjectsVM(FABPlusButtonVM fABPlusButtonVM)
        {
            FABPlusButtonVM = fABPlusButtonVM;
            ProjectFolders = new ObservableCollection<ProjectFolderVM>();

            ProjectFolders.Add(new ProjectFolderVM() { Name = "First Project" });
            RegisterEvents();
        }

        public FABPlusButtonVM FABPlusButtonVM { get; }

        private void RegisterEvents()
        {
            foreach (ProjectFolderVM folder in ProjectFolders)
            {
                folder.OnProjectFolderOpen += Folder_OnProjectFolderOpen;
            }
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
