using Ninject.Modules;
using Shophoto.Buttons;
using Shophoto.Services;
using Shophoto.Views;
using Shophoto.Views.Collections;
using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.ViewModels
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<FABPlusButtonVM>().ToSelf().InTransientScope();
            Bind<SideMenuVM>().ToSelf().InTransientScope();
            Bind<UploadVM>().ToSelf().InTransientScope();
            Bind<CollectionsVM>().ToSelf().InTransientScope();
            Bind<MainViewVM>().ToSelf().InTransientScope();
            Bind<MainWindowVM>().ToSelf().InSingletonScope();
            Bind<ImageService>().ToSelf().InSingletonScope();
        }
    }
}
