using Shophoto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Image.Thumbnail
{
    public class ImageThumbnailUploadVM : ImageThumbnailVM
    {

        private readonly ICollection<ImageThumbnailUploadVM> _imageThumbnailUploadVMs;
        public ImageThumbnailUploadVM(ICollection<ImageThumbnailUploadVM> imageThumbnailUploadVMs) : base()
        {
            _imageThumbnailUploadVMs = imageThumbnailUploadVMs;
            base.QuickButtonState = QuickButtonState.Delete;
        }

        private ICommand _deleteQuickButtonClick;
        public override ICommand DeleteQuickButtonClick
        {
            get
            {
                return _deleteQuickButtonClick ?? (_deleteQuickButtonClick = new CommandHandler(() =>
                {
                    _imageThumbnailUploadVMs.Remove(this);
                }));
            }
        }
        
    }
}
