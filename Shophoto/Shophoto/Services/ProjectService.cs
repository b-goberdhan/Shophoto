using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Services
{
    public class ProjectService
    {
        public ProjectService()
        {
            AllProjects = new List<ProjectFolderVM>();

        }


        public List<ProjectFolderVM> AllProjects
        {
            get;
            private set;
        }


         
    }
}
