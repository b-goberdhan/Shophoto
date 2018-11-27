using Ninject.Modules;
using Shophoto.Buttons;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.Services;
using Shophoto.Views;
using Shophoto.Views.Collections;
using Shophoto.Views.Collections.Aux;

namespace Shophoto.ViewModels
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<FABPlusButtonVM>().ToSelf().InTransientScope();
            Bind<SideMenuVM>().ToSelf().InTransientScope();
            Bind<UploadVM>().ToSelf().InTransientScope();
            Bind<SearchBoxVM>().ToSelf().InTransientScope();
            Bind<SortDropdownMenuVM>().ToSelf().InTransientScope();

            Bind<CollectionsVM>().ToSelf().InTransientScope();
            Bind<MainViewVM>().ToSelf().InTransientScope();
            Bind<MainWindowVM>().ToSelf().InSingletonScope();
            Bind<ImageService>().ToSelf().InSingletonScope();
        }
    }
}
