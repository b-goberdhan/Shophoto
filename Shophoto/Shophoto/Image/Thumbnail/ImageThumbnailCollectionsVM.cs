using Shophoto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Image.Thumbnail
{
    public class ImageThumbnailCollectionsVM : ImageThumbnailVM
    {
        public ImageThumbnailCollectionsVM() : base()
        {
            base.QuickButtonState = QuickButtonState.ThreeDot;
        }

        private ICommand _threeDotQuickButtonClick;
        public override ICommand ThreeDotButtonClick
        {
            get
            {
                return _threeDotQuickButtonClick ?? (_threeDotQuickButtonClick = new CommandHandler(() =>
                {
                    //put code for click here!
                }));
            }
        }

    }
}
