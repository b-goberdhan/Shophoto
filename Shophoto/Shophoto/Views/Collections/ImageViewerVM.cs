using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shophoto.Views.Collections
{
    public class ImageViewerVM : BaseVM
    {
        public event EventHandler OnClickLeft;
        public event EventHandler OnClickRight;
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();

            }
        }

        private ImageThumbnailCollectionsVM _image;
        public ImageThumbnailCollectionsVM Image
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isLeftEnabled;
        public bool IsLeftEnabled
        {
            get { return _isLeftEnabled; }
            set
            {
                _isLeftEnabled = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isRightEnabled;
        public bool IsRightEnabled
        {
            get { return _isRightEnabled; }
            set
            {
                _isRightEnabled = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                }));
            }
        }

        private ICommand _onClickLeftCommand;
        public ICommand OnClickLeftCommand
        {
            get
            {
                return _onClickLeftCommand ?? (_onClickLeftCommand = new CommandHandler(() =>
                {
                    OnClickLeft?.Invoke(this, null);   
                }));
                    
            }
        }

        private ICommand _onClickRightCommand;
        public ICommand OnClickRightCommand
        {
            get
            {
                return _onClickRightCommand ?? (_onClickRightCommand = new CommandHandler(() =>
                {
                    OnClickRight?.Invoke(this, null);
                }));
            }
        }
    }
}
