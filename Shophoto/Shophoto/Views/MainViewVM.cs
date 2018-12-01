using Shophoto.Menus;
using Shophoto.Views.Projects;

namespace Shophoto.Views
{

    public class MainViewVM
    {
        public MainViewVM(
            ProjectsVM projectsVM)
        {
            ProjectsVM = projectsVM;
        }

        public ProjectsVM ProjectsVM { get; }

        public bool OnCollectionView
        {
            get
            {
                return true;
            }
        }
    }
}
