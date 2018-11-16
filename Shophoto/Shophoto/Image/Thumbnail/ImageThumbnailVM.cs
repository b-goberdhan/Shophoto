using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shophoto.Image.Thumbnail
{
    public class ImageThumbnailVM : BaseVM
    {
        public ImageThumbnailVM()
        {

        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _leftClick;
        public ICommand LeftClick
        {
            get
            {
                return _leftClick ?? (_leftClick = new CommandHandler(() =>
                {
                    object o = this;
                    int i = 0;
                }));
            }
        }

        private ICommand _rightClick;
        public ICommand RightClick
        {
            get
            {
                return _rightClick ?? (_rightClick = new CommandHandler(() =>
                {
                    int i = 1;
                }));
            }
        }
    }
}
