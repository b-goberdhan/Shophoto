using Ninject.Modules;
using Shophoto.Buttons;
using Shophoto.Menus;
using Shophoto.Views;
using Shophoto.Views.Collections;
using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Projects;

namespace Shophoto.ViewModels
{
    public class NinjectLocator
    {
        public static FABPlusButtonVM FABPlusButtonVM
        {
            get { return IocKernel.Get<FABPlusButtonVM>(); }
        }

        public static SideMenuVM SideMenuVM
        {
            get { return IocKernel.Get<SideMenuVM>(); }
        }

        public static UploadVM UploadVM
        {
            get { return IocKernel.Get<UploadVM>(); }
        }
        
        public static ProjectsVM ProjectsVM
        {
            get { return IocKernel.Get<ProjectsVM>(); }
        }

        public static CollectionsVM CollectionsVM
        {
            get { return IocKernel.Get<CollectionsVM>(); }
        }

        public static MainViewVM MainViewVM
        {
            get { return IocKernel.Get<MainViewVM>(); }
        }

        public static MainWindowVM MainWindowVM
        {
            get { return IocKernel.Get<MainWindowVM>(); }
        }
    }
}
