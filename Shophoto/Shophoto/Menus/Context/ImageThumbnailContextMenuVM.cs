using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Menus.Context
{
    public class ImageThumbnailContextMenuVM : ContextMenuVM
    {
        
        public ImageThumbnailContextMenuVM(
            Action onEditClicked, 
            Action onDeleteClicked,
            Action onDownloadClicked) : base()
        {
            MenuItems.Add(new ContextMenuItemVM("Edit", onEditClicked));
            MenuItems.Add(new ContextMenuItemVM("Delete", onDeleteClicked));
            MenuItems.Add(new ContextMenuItemVM("Download", onDeleteClicked));
        }
    }
}
