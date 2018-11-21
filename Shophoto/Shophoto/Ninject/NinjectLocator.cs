using Ninject.Modules;
using Shophoto.Buttons;
using Shophoto.Menus;
using Shophoto.Views;
using Shophoto.Views.Collections;
using Shophoto.Views.Collections.Aux;

namespace Shophoto.ViewModels
{
    public class NinjectLocator
    {
        public FABPlusButtonVM FABPlusButtonVM
        {
            get { return IocKernel.Get<FABPlusButtonVM>(); }
        }

        public SideMenuVM SideMenuVM
        {
            get { return IocKernel.Get<SideMenuVM>(); }
        }

        public UploadVM UploadVM
        {
            get { return IocKernel.Get<UploadVM>(); }
        }
        
        public CollectionsVM CollectionsVM
        {
            get { return IocKernel.Get<CollectionsVM>(); }
        }

        public MainViewVM MainViewVM
        {
            get { return IocKernel.Get<MainViewVM>(); }
        }

        public MainWindowVM MainWindowVM
        {
            get { return IocKernel.Get<MainWindowVM>(); }
        }
    }
}
