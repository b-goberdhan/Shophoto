using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Services
{
    public class ProjectService
    {
        public ProjectService()
        {
            Projects = new ObservableCollection<ProjectFolderVM>();
        }


        public ObservableCollection<ProjectFolderVM> Projects
        {
            get;
            private set;
        }




         
    }
}
