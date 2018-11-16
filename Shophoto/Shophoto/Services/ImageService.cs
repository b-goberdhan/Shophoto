using Shophoto.Image.Thumbnail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Services
{
    public class ImageService
    {

        public ImageService()
        {
            CollectionImages = new ObservableCollection<ImageThumbnailVM>();
        }

        public ObservableCollection<ImageThumbnailVM> CollectionImages
        {
            get;
            private set;
        }
    }
}
